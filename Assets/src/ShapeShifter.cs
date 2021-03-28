using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeShifter : MonoBehaviour
{


    public Mesh shiftTo;
    private MeshFilter meshFilter;
    private int parentID;



    // Start is called before the first frame update
    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        parentID = transform.parent.gameObject.GetInstanceID();
        GameEvents.events.OnShapeShiftTrigger += OnShapeShift;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    ///<summary>Begin shape shift event.</summary>
    private void OnShapeShift(int id)
    {
        if (id == parentID)
            Shift();
    }


    private void Shift()
    {
        GameEvents.events.SinkTrigger();
        Mesh mesh = Instantiate(shiftTo);
        meshFilter.sharedMesh = mesh;
    }
}
