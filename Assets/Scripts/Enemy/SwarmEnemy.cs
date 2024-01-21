using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmEnemy : Enemy
{
	// Start is called before the first frame update
	protected override void Start()
	{
		base.Start();
	}

	// Update is called once per frame
	protected override void Update()
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
}
