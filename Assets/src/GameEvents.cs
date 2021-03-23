using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{


    public static GameEvents events;
    public event Action onDoorwayTriggerEnter;
    public event Action onDoorwayTriggerExit;


    private void Awake()
    {
        events = this;
    }


    public void DoorwayTriggerEnter()
    {
        if (onDoorwayTriggerEnter != null)
        {
            onDoorwayTriggerEnter();
        }
    }


    public void DoorwayTriggerExit()
    {
        if(onDoorwayTriggerExit != null)
        {
            onDoorwayTriggerExit();
        }
    }
}
