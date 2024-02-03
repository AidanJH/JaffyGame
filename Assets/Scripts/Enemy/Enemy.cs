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
	
	private void OnCollisionEnter2D(Collision2D collision)
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

	private void RotateTowardsPlayer()
	{
		// Direction vector from the enemy to the player
		Vector2 direction = (player.position - transform.position).normalized;
		// The angle from the enemy to the player
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
		// From this angle, calculate the target rotation
		Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
		
		// Smoothly transition between current rotation and the player's position
		transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
	}

	private void MoveTowardsPlayer()
	{
		// Moves the enemy towards the player
		transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
	}
}


// using UnityEngine;

// public class YourEnemyScript : MonoBehaviour
// {
//     public float rotationSpeed = 5.0f; // Adjust the speed as needed
//     public float correctionDuration = 1.0f; // Adjust the duration of correction as needed

//     private Transform player;

//     private void Start()
//     {
//         player = /* Get the player's transform here */;
//     }

//     private void Update()
//     {
//         RotateTowardsPlayer();
//     }

//     private void RotateTowardsPlayer()
//     {
//         // Direction vector from the enemy to the player
//         Vector2 direction = (player.position - transform.position).normalized;
//         // The angle from the enemy to the player
//         float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

//         // Check if the angle is significantly different from the current rotation
//         if (Quaternion.Angle(transform.rotation, Quaternion.Euler(new Vector3(0f, 0f, angle))) > 5.0f)
//         {
//             // If it is, correct the rotation over time
//             StartCoroutine(CorrectRotation(angle));
//         }
//         else
//         {
//             // Otherwise, smoothly transition between current rotation and the player's position
//             transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(0f, 0f, angle)), rotationSpeed * Time.deltaTime);
//         }
//     }

//     private IEnumerator CorrectRotation(float targetAngle)
//     {
//         Quaternion currentRotation = transform.rotation;
//         Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, targetAngle));

//         float elapsedTime = 0f;

//         while (elapsedTime < correctionDuration)
//         {
//             transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, elapsedTime / correctionDuration);
//             elapsedTime += Time.deltaTime;
//             yield return null;
//         }

//         transform.rotation = targetRotation; // Ensure the final rotation is exactly the target rotation
//     }
// }