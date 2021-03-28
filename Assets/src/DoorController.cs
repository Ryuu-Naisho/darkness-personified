using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private Vector3 initialPosition;
    private int parentID;



    // Start is called before the first frame update
    void Start()
    {
        parentID = transform.parent.gameObject.GetInstanceID();
        initialPosition = transform.position;
        GameEvents.events.onDoorwayTriggerEnter += OnDoorwayOpen;
        GameEvents.events.onDoorwayTriggerExit += OnDoorwayClose;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    ///<summary>Set door to open position.</summary>
    ///<param name="id">int id, instance id of the gameObject.</param>
    private void OnDoorwayOpen(int id)
    {
        if (id == parentID)
            transform.position += Vector3.up * 5;
    }


    ///<summary>Set door to closed position.</summary>
    ///<param name="id">int id, instance id of the gameObject.</param>
    private void OnDoorwayClose(int id)
    {
        if (id == parentID)
            transform.position = initialPosition;
    }


    ///<summary>Get the ID of the parent object.</summary>
    public int GetID()
    {
        return this.parentID;
    }
}
