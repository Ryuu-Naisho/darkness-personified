using System;
using System.Collections;  
using System.Collections.Generic;
using System.Linq; 
using UnityEngine;

public class wCalc
{


    ///<summary>Get the closest gameobject.</summary>
    ///<param name="distance">Array of distances from the desired target.</param>
    ///<param name="neighboors">Array of gameobjects.</param>
    ///<return>GameObject, closest gameobject to your target.</return>
    public GameObject GetClosest(List<float> distances, GameObject[] neighboors)
    {
        int index;
        float closestDistance = distances.Min();
        index = distances.FindIndex(distance => distance == closestDistance);
        GameObject closestNeighbor = neighboors[index];
        return closestNeighbor;
    }
}
