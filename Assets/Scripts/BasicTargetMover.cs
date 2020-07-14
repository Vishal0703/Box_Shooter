using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTargetMover : MonoBehaviour
{
    public float spinSpeed = 180.0f;
    public float motionMagnitude = 0.1f;
    public bool doSpin = true;
    public bool doMotion = false;

    // Update is called once per frame
    void Update()
    {
        //rotate gameObject
        if(doSpin)
        gameObject.transform.Rotate(new Vector3(0,1,0)*spinSpeed*Time.deltaTime);
        
        //translate gameObject
        if(doMotion)
        gameObject.transform.Translate(Vector3.up*Mathf.Cos(Time.timeSinceLevelLoad)*motionMagnitude);
    }
}
