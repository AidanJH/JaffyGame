using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{ 
    public float acceleration = 100f;
    public float rotationSpeed = 10f;
    public float maxSpeed = 10f;
    public float deceleration = 5f;  // New field for deceleration
    private Vector2 currentVelocity;
    private Rigidbody2D rb;

    private Quaternion targetRotation;
    private bool isRotating;

   

    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

     void LateUpdate()
    {
        Vector2 direction = Gamepad.current.leftStick.ReadValue();

        UpdateVelocity(direction);
        UpdateTargetRotation();
    }

    private void UpdateTargetRotation()
    {
        float rotationAmount = Gamepad.current.rightTrigger.ReadValue() - Gamepad.current.leftTrigger.ReadValue();
        transform.Rotate(Vector3.back, rotationAmount * rotationSpeed * Time.deltaTime);
 
    }

    private void UpdateVelocity(Vector2 direction)
    {
        if (direction.sqrMagnitude > 0)
        {
            Vector2 accelerationVector = direction * acceleration;
            currentVelocity += accelerationVector * Time.deltaTime;
        }
        else
        {
            // Apply deceleration when joystick is not being moved
            currentVelocity -= currentVelocity * deceleration * Time.deltaTime;
        }

        // Cap the speed if the current velocity exceeds maxSpeed
        if (currentVelocity.magnitude > maxSpeed)
        {
            currentVelocity = currentVelocity.normalized * maxSpeed;
        }

        rb.velocity = currentVelocity;
    }

    private void RotateShip()
    {
        if (isRotating)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

}
