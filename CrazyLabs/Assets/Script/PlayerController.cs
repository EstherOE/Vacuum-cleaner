using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class PlayerController : MonoBehaviour
{
    public Joystick joyStick;
    public float speed = 5.0f;
    public float rotationSpeed;
    
    // Update is called once per frame
    void Update()
    { //move the player
        float horizontalInput = joyStick.Horizontal;
        float verticalInput = joyStick.Vertical;

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        movementDirection.Normalize();

        transform.Translate(movementDirection* Time.deltaTime * speed, Space.World);
        //look in the direction of movement
        if(movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("dirt"))
        {
            Destroy(other.gameObject);
        }
    }

}
