using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private Vector3 initialPosition;



    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        GameEvents.events.onDoorwayTriggerEnter += OnDoorwayOpen;
        GameEvents.events.onDoorwayTriggerExit += OnDoorwayClose;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnDoorwayOpen()
    {
        transform.position += Vector3.up * 10;
    }


    private void OnDoorwayClose()
    {
        transform.position = initialPosition;
    }
}
