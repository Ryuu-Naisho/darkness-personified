using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightEffects : MonoBehaviour
{


    public float lowestLightIntensity;
    public int flickerSpeed;
    public int flickerTime;
    private Light light;
    private float initialIntensity;
    private bool powered = true;
    private bool flicker = false;
    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
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



    private IEnumerator Wait(int time, Action onComplete)
    {
        yield return new WaitForSeconds(time);
        onComplete();
    }
}
