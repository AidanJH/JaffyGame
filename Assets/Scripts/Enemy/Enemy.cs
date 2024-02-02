using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
	public float moveSpeed;
	public float rotationSpeed;
	public Transform player;
	public float health;
	public float collisionDamage;

	protected virtual void Start()
	{
		SetPlayerReferenceByTag("Player");
	}

	protected virtual void Update()
	{
		if (player != null)
		{
			RotateTowardsPlayer();
			MoveTowardsPlayer();
		}
	}
	
	public void SetPlayerReferenceByTag(string playerTag)
	{
		GameObject playerObject = GameObject.FindGameObjectWithTag(playerTag);
		if (playerObject != null)
		{
			player = playerObject.transform;
		}
		else
		{
			Debug.LogError("Player not found with tag: " + playerTag);
		}
	}
	
	void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.layer == LayerMask.NameToLayer("PlayerWeapons"))
		{
			Debug.Log("Player shot enemy: " + gameObject.name);
			health -= collisionDamage;
			if (health <= 0) 
			{
				Destroy(gameObject);  
			}
		} else {
			Debug.Log("The enemy has collided with" + collision.gameObject.name);
		}	
	}

	void RotateTowardsPlayer()
	{
		// Direction vector from the enemy to the player
		Vector2 direction = (player.position - transform.position).normalized;
		// The difference in angle between the player and enemy
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
		// From this angle, calculate the target rotation
		Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
		
		// Smoothly transition between current rotation and the player's position
		transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
	}

	void MoveTowardsPlayer()
	{
		// Moves the enemy towards the player
		transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
	}
}