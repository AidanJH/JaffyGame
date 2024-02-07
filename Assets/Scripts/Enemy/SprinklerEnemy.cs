using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprinklerEnemy : Enemy
{
	public List<Weapon> weapons;
	public float minDistanceFromPlayer = 10f;

	protected override void FixedUpdate()
	{
		base.FixedUpdate();
		RotateIdle();
		ShootInSprayingPattern();
	}

	protected override void RotateTowardsPlayer(){}
	
	protected override void MoveTowardsPlayer() 
	{		
		float distance = Vector2.Distance(transform.position, player.position);
		
		// Stop chasing when it reaches the minimum distance from the player
		if (distance > minDistanceFromPlayer) 
		{
			transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
		}
	}
	
	private void ShootInSprayingPattern() 
	{
		foreach (Weapon weapon in weapons) 
		{
			float angle = weapon.transform.rotation.eulerAngles.z;
			weapon.WeaponShoot(angle);
		}
	}
	
	private void RotateIdle() 
	{
		transform.Rotate(new Vector3(0, 0, rotationSpeed) * Time.deltaTime);
	}
}
