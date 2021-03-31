using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class LightEffects : MonoBehaviour
{


    public float lowestLightIntensity;
    public int flickerSpeed;
    public int flickerTime;
    public AudioClip[] clips;
    private Light light;
    private float initialIntensity;
    private bool powered = true;
    private bool flicker = false;
    private AudioSource audioSource;
    private bool audio_play;
    private bool audio_toggleChange;



    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
        audioSource = GetComponent<AudioSource>();
        initialIntensity = light.intensity;
        GameEvents.events.OnLightFlickerTrigger += Flicker;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown("l"))
        //    ToggleLight();
        //else if (Input.GetKeyDown("f"))
        //    flicker = flicker ? false : true;



        if (flicker)
        {
            Action flickerLights = ()=> ToggleLight();
            StartCoroutine(Wait(flickerSpeed, flickerLights));
            StopFlicker();
        }
    }


    ///<summary>Toggle light on and off.</summary>
    private void ToggleLight()
    {


        if (powered)
            LightOff();
        else
            LightOn();


        powered = powered ? false : true;        
    }



    ///<summary>Turn light off.<summary>
    private void LightOff()
    {
        light.intensity = lowestLightIntensity;
    }


    ///<summary>Turn light on.</summary>
    private void LightOn()
    {
        int index = UnityEngine.Random.Range(0, clips.Length);
        PlayClip(clips[index]);
        light.intensity = initialIntensity;
    }


    ///<summary>Flicker lights.</summary>
    private void Flicker()
    {
        this.flicker = true;
    }


    ///<summary>Send signal to stop flicker.</summary>
    private void StopFlicker()
    {
        Action stopFlicker = ()=> 
        {
            this.flicker = false;
            LightOn();
        };
        StartCoroutine(Wait(flickerTime, stopFlicker));
    }


        ///<summary>Play audio clip once.</summary>
    ///<param name="clip">AudioClip to play.</param>
    private void PlayClip(AudioClip clip)
    {
        audio_play = true;
        audio_toggleChange = true;
        //Check if you just set the toggle to positive.
        if (audio_play == true && audio_toggleChange == true)
        {
            audioSource.clip = clip;
            audioSource.Play();
            audio_toggleChange = false;
        }
        //Check if you just set the toggle to false
        if (audio_play == false && audio_toggleChange == true)
        {
            //Stop the audio
            audioSource.Stop();
            //Ensure audio doesn't play more than once
            audio_toggleChange = false;
        }
    }



    private IEnumerator Wait(int time, Action onComplete)
    {
        yield return new WaitForSeconds(time);
        onComplete();
    }
}
