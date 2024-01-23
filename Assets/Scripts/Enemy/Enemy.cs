using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public float moveSpeed = 0.5f;
	public float rotationSpeed = 3f;
	private Transform player;
	
	public float health = 100f;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;

		if (player == null)
		{
			Debug.LogError("Player not found!");
		}
	}

	void Update()
	{
		if (player != null)
		{
			RotateTowardsPlayer();

			MoveTowardsPlayer();
		}
	}

	void RotateTowardsPlayer()
	{
		Vector3 direction = (player.position - transform.position).normalized;

		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

		Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
	}

	void MoveTowardsPlayer()
	{
		Vector3 direction = (player.position - transform.position).normalized;

		Vector3 newPosition = transform.position + direction * moveSpeed * Time.deltaTime;

		transform.position = newPosition;
	}
	
	void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.layer == LayerMask.NameToLayer("PlayerWeapons"))
		{
			Debug.Log("Player shot enemy: " + gameObject.name);
			health -= 25f;
			if (health <= 0) 
			{
				Destroy(gameObject);  
			}
		} else {
			Debug.Log("The enemy has collided with" + collision.gameObject.name);
		}	
	}
}