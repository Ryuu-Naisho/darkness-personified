﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeShifter : MonoBehaviour
{


    public Mesh shiftTo;
    public float shiftTime;
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
        Action doShift = () => meshFilter.sharedMesh = mesh;
        StartCoroutine(Wait(shiftTime, doShift));
    }


        private IEnumerator Wait(float time, Action onComplete)
    {
        yield return new WaitForSeconds(time);
        onComplete();
    }
}
