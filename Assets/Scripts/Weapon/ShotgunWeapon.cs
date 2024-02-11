using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunWeapon : Weapon
{
	[SerializeField] private int pelletsPerShot = 5;
	[SerializeField] private float spreadAngle = 30f; // The total spread angle of the shotgun blast
	[SerializeField] private float startOffset = 15f; // The offset from the center angle to start the spread. To be centred, it should be half of the spreadAngle
	
	private void Start()
	{
		projectilePrefab.projectileForce = 6f;	
	}

	public override void WeaponShoot(float firingAngle)
	{
		if (reloaded) 
		{
			FireShotgunBlast(firingAngle);
			reloaded = false;
			StartCoroutine(ReloadWeapon());
		}
		else 
		{
			Debug.Log("Weapon not reloaded yet");
		}
		
	}
	
	private void FireShotgunBlast(float firingAngle) 
{
	// Calculate the angle increment for each pellet
	float angleIncrement = spreadAngle / (pelletsPerShot - 1); // Adjust this value to control the spread

	for (int i = 0; i < pelletsPerShot; i++) 
	{
		// Calculate the adjusted firing angle for this pellet
		float adjustedAngle = firingAngle + (i * angleIncrement) - startOffset; // Start from left to right, adjust the 5f based on your preference

		// Create the projectile with the adjusted firing angle
		Projectile firedProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
		firedProjectile.gameObject.layer = gameObject.layer;
		firedProjectile.Launch(adjustedAngle);
	}
}
}
