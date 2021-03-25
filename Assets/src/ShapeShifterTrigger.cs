using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeShifterTrigger : MonoBehaviour
{


    private int parentID;
    // Start is called before the first frame update
    void Start()
    {
        parentID = transform.parent.gameObject.GetInstanceID();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    ///<summary>Trigger when a player is in proximity.</summary>
    private void OnTriggerEnter(Collider other)
    {
        GameEvents.events.ShapeShiftTrigger(parentID);
    }
}
