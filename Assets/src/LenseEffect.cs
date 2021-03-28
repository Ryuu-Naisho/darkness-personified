using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class LenseEffect : MonoBehaviour
{
    public float maxFieldOfView;
    public float sinkEffectRate;
    public AudioClip bassDrop;
    private AudioSource audioSource;
    private float initialFieldOfView;
    private float currentFieldOfView;
    private Camera camera;
    private bool isSinking = false;
    private bool audio_play;
    private bool audio_toggleChange;



    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
        audioSource = GetComponent<AudioSource>();
        initialFieldOfView = camera.fieldOfView;
        currentFieldOfView = initialFieldOfView;
        GameEvents.events.OnSinkTrigger += DoSink;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("c") && !isSinking)
            DoSink();


            
        if (isSinking)
        {
            currentFieldOfView += sinkEffectRate;
            Sink();
        }
    }


    ///<summary>Send signal to do sink.</summary>
    private void DoSink()
    {
        this.isSinking = true;
        PlayClip(bassDrop);
    }



    ///<summary>Perform sink effect with the lense. If the effect reached it's max
    ///Revert back to the initial param.</summary>
    private void Sink()
    {
        if (currentFieldOfView > maxFieldOfView)
        {
            isSinking = false;
            currentFieldOfView = initialFieldOfView;
        }
        camera.fieldOfView = currentFieldOfView;
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
}
