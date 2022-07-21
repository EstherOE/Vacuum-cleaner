using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickPlayerExample : MonoBehaviour
{
    public float speed;
    public VariableJoystick variableJoystick;
    public Rigidbody rb;
    Vector3 direction = Vector3.zero;




    private void Update()
    {
        float horizontalInput = variableJoystick.Horizontal;
        float verticalInput = variableJoystick.Vertical;
        direction = new Vector3(horizontalInput, 0, verticalInput).normalized;
    }


    public void FixedUpdate()
    {

        // direction = Vector3.forward * verticalInput + Vector3.right * horizontalInput;
        if (direction != Vector3.zero)
        {
            rb.MovePosition(transform.position + direction * speed * Time.fixedDeltaTime);
        }
        
        //rb.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
    }
}