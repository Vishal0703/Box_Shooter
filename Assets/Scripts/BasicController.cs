using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicController : MonoBehaviour
{
    private CharacterController myController;
    public float moveSpeed = 3.0f;
    public float gravity = 9.81f;
    private void Start()
    { 
        myController = GetComponent<CharacterController>();
        
    }
    // Update is called once per frame
    
    void Update()
    {
        Vector3 mvz = Vector3.forward*moveSpeed*Time.deltaTime*Input.GetAxis("Vertical");
        Vector3 mvx = Vector3.right*moveSpeed*Time.deltaTime*Input.GetAxis("Horizontal");

        Vector3 mv = transform.TransformDirection(mvx + mvz);

        mv.y -=  (float)0.5* gravity * Mathf.Pow(Time.deltaTime, 2);
        myController.Move(mv);

    }
}
