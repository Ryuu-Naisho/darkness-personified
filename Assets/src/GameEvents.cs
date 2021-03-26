using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{


    public static GameEvents events;
    public event Action<int> onDoorwayTriggerEnter;
    public event Action<int> onDoorwayTriggerExit;
    public event Action<int> OnShapeShiftTrigger;


    private void Awake()
    {
        events = this;
    }


    ///<summary>Trigger door enter event.</summary>
    ///<param name="id">int id, instance id of gameObject.</param>
    public void DoorwayTriggerEnter(int id)
    {
        if (onDoorwayTriggerEnter != null)
        {
            onDoorwayTriggerEnter(id);
        }
    }


    ///<summary>Trigger door exit event.</summary>
    ///<param name="id">int id, instance id of gameObject.</param>
    public void DoorwayTriggerExit(int id)
    {
        if(onDoorwayTriggerExit != null)
        {
            onDoorwayTriggerExit(id);
        }
    }


    ///<summary>Trigger shape shift event.</summary>
    ///<param name="id">int id, instance id of gameObject.</param>
    public void ShapeShiftTrigger(int id)
    {
        if (OnShapeShiftTrigger != null)
            OnShapeShiftTrigger(id);
    }
}
