using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBob : MonoBehaviour
{


    public float bobbingRate;
    private float defaultPosY = 0;
    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        defaultPosY = transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    
    ///<summary>Slowly rest head from bouncing.</summary>
    ///<param name="speed">Walking speed of the object.</param>
    public void Idle(float speed)
    {
        timer = 0;
        transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Lerp(transform.localPosition.y, defaultPosY, Time.deltaTime * speed), transform.localPosition.z);
    }


    ///<summary>Bounce transform by bobbingRate and speed.</summary>
    ///<param name="speed">Speed of object.</param>
    public void Bounce(float speed)
    {
        timer += Time.deltaTime * speed;
        transform.localPosition = new Vector3(transform.localPosition.x, defaultPosY + Mathf.Sin(timer) * bobbingRate, transform.localPosition.z);
    }
}
