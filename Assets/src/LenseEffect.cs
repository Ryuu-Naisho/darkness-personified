using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LenseEffect : MonoBehaviour
{
    public float maxFieldOfView;
    public float sinkEffectRate;
    private float initialFieldOfView;
    private float currentFieldOfView;
    private Camera camera;
    private bool isSinking = false;



    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
        initialFieldOfView = camera.fieldOfView;
        currentFieldOfView = initialFieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("c") && !isSinking)
            isSinking = true;



        if (isSinking)
        {
            currentFieldOfView += sinkEffectRate;
            Sink();
        }
    }


    ///<summary>Perform sink effect with the lense. If the effect reached it's max
    ///Revert back to the initial param.</summary>
    private void Sink()
    {
        if (currentFieldOfView > maxFieldOfView)
        {
            isSinking = false;
            currentFieldOfView = initialFieldOfView;
        }
        camera.fieldOfView = currentFieldOfView;
    }
}
