using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameEvents.events.DoorwayTriggerEnter();
    }


    private void OnTriggerExit(Collider other)
    {
        GameEvents.events.DoorwayTriggerExit();
    }
}
