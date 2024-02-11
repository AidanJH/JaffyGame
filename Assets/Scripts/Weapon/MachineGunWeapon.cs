using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunWeapon : BurstWeapon
{
	[SerializeField] private float maxAngle; // The maximum angle off target that a bullet can travel
	
	protected override void Start() 
	{
		bulletsLeftInMagazine = magazineSize;
		projectilePrefab.projectileForce = 10f;
	}
	
	protected override IEnumerator FireProjectile(float firingAngle)
	{
		isFiring = true;
		Projectile firedProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
		firedProjectile.gameObject.layer = gameObject.layer;
		
		// Generate a random angle within the maximum angle
		float randomOffset = Random.Range(-maxAngle / 2f, maxAngle / 2f);

		// Calculate the adjusted firing angle for this pellet
		float adjustedAngle = firingAngle + randomOffset;
		
		firedProjectile.Launch(adjustedAngle);
		bulletsLeftInMagazine--;
		
		yield return StartCoroutine(DelayBetweenShots());
		isFiring = false;
	}
}
