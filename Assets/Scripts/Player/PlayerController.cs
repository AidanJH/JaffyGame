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
	private PlayerInputActions playerInputActions;

	private Quaternion targetRotation;
	private bool isRotating;

	public float health = 100f;

	private void Awake() {
		rb = GetComponent<Rigidbody2D>();
		playerInputActions = new PlayerInputActions();
		playerInputActions.Player.Enable();
	}

	void FixedUpdate()
	{
		UpdateVelocity();
		UpdateTargetRotation();
	}

	private void UpdateTargetRotation()
	{
        float rotationAmount = playerInputActions.Player.Rotate.ReadValue<float>();
        transform.Rotate(Vector3.back, rotationAmount * rotationSpeed * Time.deltaTime); 
	}

	private void UpdateVelocity()
	{
        Vector2 direction = playerInputActions.Player.Move.ReadValue<Vector2>();

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

	void OnCollisionEnter2D(Collision2D collision) 
	{
		if(collision.gameObject.layer == LayerMask.NameToLayer("PlayerWeapons"))
		{
			Debug.Log("You shot yourself dummy");
		} else {
			health -= 25f;
			if (health <= 0) 
			{
				Destroy(gameObject);  
			}
		}
	}
}
