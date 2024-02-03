using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : Enemy
{
	// public Weapon weapon;

	protected override void Update()
	{
			base.Update();
			// ShootAtPlayer();
	}

	// Function for stopping at a certain distance away from the player

	// Function for calculating the direction to the player
	// private void ShootAtPlayer() 
	// {
	// 	// Vector from this enemy to player
	// 	Vector2 direction = (player.position - transform.position).normalized;
	// 	// The angle from this enemy to the player
	// 	float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
	// 	//Converts the angle to be within the range of 0,360. By turning it positive and finding the remainder.
	// 	// Debug.Log(angle);
	// 	angle = (angle + 360) % 360;
	// 	// Tell the weapon to shoot at that angle
	// 	weapon.WeaponShoot(angle);
	// }
}


