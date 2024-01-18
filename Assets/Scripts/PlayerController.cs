// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.InputSystem;

// public class PlayerController : MonoBehaviour
// { 
//     public float acceleration = 100f;
//     public float rotationSpeed = 1000f;
//     public float maxSpeed = 10f;
//     public float deceleration = 5f;  // New field for deceleration
//     private Vector2 currentVelocity;
//     private Rigidbody2D rb;
//     private Gamepad gamepad;
//     private Quaternion targetRotation;
//     private bool isRotating;

//     private Generator generator;
//     private Shield shield;
//     private Engine engine;

//     private List<Weapon> weapons;
//     public WeaponController WeaponController;



//     // Start is called before the first frame update
//     void Start()
//     {
//         rb = GetComponent<Rigidbody2D>();
        

//         if (Gamepad.all.Count > 0)
//         {
//             gamepad = Gamepad.all[0];
//         }
//     }

//      void LateUpdate()
//     {
//         if (gamepad == null)
//         {
            
//         }

//         Vector2 direction = gamepad.leftStick.ReadValue();
//         UpdateTargetRotation(direction);
//         UpdateVelocity(direction);
//         RotateShip();

        
//     }

//     private void UpdateTargetRotation(Vector2 direction)
//     {
//         // Only update target rotation and set isRotating to true if joystick is being moved
//         if (direction.sqrMagnitude > 0)
//         {
//             float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
//             targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle - 90f));
//             isRotating = true;
//         }
//         else
//         {
//             isRotating = false;
//         }
//     }

//     private void UpdateVelocity(Vector2 direction)
//     {
//         if (direction.sqrMagnitude > 0)
//         {
//             Vector2 accelerationVector = direction * acceleration;
//             currentVelocity += accelerationVector * Time.deltaTime;
//         }
//         else
//         {
//             // Apply deceleration when joystick is not being moved
//             currentVelocity -= currentVelocity * deceleration * Time.deltaTime;
//         }

//         // Cap the speed if the current velocity exceeds maxSpeed
//         if (currentVelocity.magnitude > maxSpeed)
//         {
//             currentVelocity = currentVelocity.normalized * maxSpeed;
//         }

//         rb.velocity = currentVelocity;
//     }

//     private void RotateShip()
//     {
//         if (isRotating)
//         {
//             transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
//         }
//     }

// }
