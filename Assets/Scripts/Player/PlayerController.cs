using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{ 
	private float _acceleration = 100f;
	private float _rotation = 10f;
	public float maxSpeed = 10f;
	public float deceleration = 5f;  // New field for deceleration
	private Vector2 currentVelocity;
	private Rigidbody2D rb;

	private Quaternion targetRotation;
	private bool isRotating;

	public float health = 100f;

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
		transform.Rotate(Vector3.back, rotationAmount * _rotation * Time.deltaTime);
 
	}

	private void UpdateVelocity(Vector2 direction)
	{
		if (direction.sqrMagnitude > 0)
		{
			Vector2 accelerationVector = direction * _acceleration;
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
			transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotation * Time.deltaTime);
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

	//The following 'ModifyX' methods can be used with a negative or positive integer
	//TODO: Add percentage increases later if needed
	//TODO: Change return type to bool and add guards if needed, so we can know whether adding a modifier is permitted
	public void ModifySpeed(int speed){
		maxSpeed += speed;
	}

	public void ModifyAcceleration(int acceleration){
		_acceleration += acceleration;
	}

	public void ModifyRotation(int rotation){
		_rotation += rotation;
	}
}
