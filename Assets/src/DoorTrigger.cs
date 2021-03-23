using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{


    ///<summary>Trigger when an object is near the door. Trigger open.</summay>
    private void OnTriggerEnter(Collider other)
    {
        GameEvents.events.DoorwayTriggerEnter();
    }


    ///<summary>Trigger when an object is exiting the door. Trigger close.</summary>
    private void OnTriggerExit(Collider other)
    {
        GameEvents.events.DoorwayTriggerExit();
    }
}
