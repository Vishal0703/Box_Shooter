using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTargetMover : MonoBehaviour
{
    // Start is called before the first frame update
    public enum motionDirections
    {
        Spin,
        Horizontal,
        Vertical
    };

    public motionDirections motionState = motionDirections.Horizontal;
    public float spinSpeed = 180.0f;
    public float motionMagnitude = 0.1f;
    // Update is called once per frame
    void Update()
    {
        switch (motionState)
        {
            case motionDirections.Spin:
                {
                    gameObject.transform.Rotate(new Vector3(0,1,0) * (spinSpeed * Time.deltaTime));
                    break;
                }
            case motionDirections.Horizontal:
            {
                gameObject.transform.Translate(Vector3.right * (Mathf.Cos(Time.timeSinceLevelLoad) * motionMagnitude));
                break;
            }
            case motionDirections.Vertical:
            {
                gameObject.transform.Translate(Vector3.up * (Mathf.Cos(Time.timeSinceLevelLoad) * motionMagnitude));
                break;
            }
        }
    }
}
