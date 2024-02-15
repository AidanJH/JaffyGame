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
	private PlayerInputActions playerInputActions;

	public List<GameObject> anchorPoints = new List<GameObject>();

	private Quaternion targetRotation;
	private bool isRotating;
	private bool _dashActivated;
	private float _dashSpeed;
	private float _dashDuration;
	private bool _isDashing;

	public float health = 100f;

	public List<GameObject> moduleAnchorPoints = new List<GameObject>();

	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		_isDashing = false;
		//TODO: Ideally this would be set based on saved stats, e.g if you resumed a game or chose the dashing perk before starting the game
		_dashActivated = false;
	}
	
	private void Awake() {
		GetAllAnchorPoints();
		rb = GetComponent<Rigidbody2D>();
		playerInputActions = new PlayerInputActions();
		playerInputActions.Player.Enable();
	}

	void FixedUpdate()
	{
		UpdateVelocity();
		UpdateTargetRotation();

		//TODO: Merge in Keyboard controls for this
		if (Gamepad.current.xButton.wasPressedThisFrame && _dashActivated && !_isDashing)
        {
			StartDash();
        }
	}

	private void UpdateTargetRotation()
	{
        float rotationAmount = playerInputActions.Player.Rotate.ReadValue<float>();
        transform.Rotate(Vector3.back, rotationAmount * _rotation * Time.deltaTime); 
	}

	private void UpdateVelocity()
	{
        Vector2 direction = playerInputActions.Player.Move.ReadValue<Vector2>();

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

	private void GetAllAnchorPoints(){
		Transform[] transform = gameObject.GetComponentsInChildren<Transform>();
		
		 foreach (Transform child in transform)
        {
            if (child.tag == "AnchorPoint")
            {
                anchorPoints.Add(child.gameObject);
            }
        }
	}
	public void SetDash(float dashSpeed, float dashDuration){
		Debug.Log("Module effect is being applied");
		//Toggle right now
		_dashActivated = !_dashActivated;
		_dashDuration = dashDuration;
		_dashSpeed = dashSpeed;
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

	public void StartDash()
    {
        StartCoroutine(DashCoroutine(_dashSpeed, _dashDuration));
    }

    private IEnumerator DashCoroutine(float dashSpeed, float dashDuration)
    {
        _isDashing = true;
        float timeStartedDashing = Time.time;
		float originalMaxSpeed = maxSpeed;
		float originalAcceleration = _acceleration;

        // Increase the speed for the dash
        maxSpeed += dashSpeed;
		_acceleration += 100000000f;

        // Wait for the dash duration
        while (Time.time < timeStartedDashing + dashDuration)
        {
            yield return null;
        }

        // Reset the speed after dashing
        maxSpeed = originalMaxSpeed;
		_acceleration = originalAcceleration;
        _isDashing = false;
    }


}
