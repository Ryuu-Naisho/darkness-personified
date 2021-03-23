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


    ///<summary>Trigger door enter event.</summary>
    public void DoorwayTriggerEnter()
    {
        if (onDoorwayTriggerEnter != null)
        {
            onDoorwayTriggerEnter();
        }
    }


    ///<summary>Trigger door exit event.</summary>
    public void DoorwayTriggerExit()
    {
        if(onDoorwayTriggerExit != null)
        {
            onDoorwayTriggerExit();
        }
    }
}
