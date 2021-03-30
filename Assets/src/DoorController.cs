using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
public class DoorController : MonoBehaviour
{
    private Vector3 initialPosition;
    private int ID;
    private Animator animator;



    // Start is called before the first frame update
    void Start()
    {
        ID = gameObject.GetInstanceID();
        animator = GetComponent<Animator>();
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
        if (id == ID)
            animator.SetTrigger("Open");
    }


    ///<summary>Set door to closed position.</summary>
    ///<param name="id">int id, instance id of the gameObject.</param>
    private void OnDoorwayClose(int id)
    {
        if (id == ID)
            animator.SetTrigger("Close");
    }


    ///<summary>Get the ID of the parent object.</summary>
    public int GetID()
    {
        return this.ID;
    }
}
