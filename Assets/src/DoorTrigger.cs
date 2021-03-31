using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{


    private int parentID;


    void Start()
    {
        parentID = transform.parent.gameObject.GetInstanceID();
    }



    ///<summary>Trigger when an object is near the door. Trigger open.</summay>
    private void OnTriggerEnter(Collider other)
    {
        GameEvents.events.DoorwayTriggerEnter(parentID);
        int id = transform.parent.gameObject.GetInstanceID();
    }


    ///<summary>Trigger when an object is exiting the door. Trigger close.</summary>
    private void OnTriggerExit(Collider other)
    {
        GameEvents.events.DoorwayTriggerExit(parentID);
    }
}
