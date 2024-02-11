using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BurstWeapon : Weapon
{
	[SerializeField] protected int magazineSize;
	[SerializeField] private float delayBetweenShots;
	[SerializeField] protected bool isFiring = false; // Stops the FireProjectile coroutine from being called twice (shoot one bullet a time)
	protected int bulletsLeftInMagazine;
	
	protected virtual void Start() 
	{
		bulletsLeftInMagazine = magazineSize;
		projectilePrefab.projectileForce = 12f;
	}

	public override void WeaponShoot(float firingAngle) 
	{
		if (reloaded && !isFiring) 
		{
			StartCoroutine(FireProjectile(firingAngle));
		}
		else if (!reloaded)
		{
			Debug.Log("Weapon not reloaded yet");
		}
		
		if (bulletsLeftInMagazine <= 0) 
		{
			reloaded = false;
			StartCoroutine(ReloadWeapon());
			bulletsLeftInMagazine = magazineSize;
		}
	}
	
	protected virtual IEnumerator FireProjectile(float firingAngle) 
	{
		isFiring = true;
		Projectile firedProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
		firedProjectile.gameObject.layer = gameObject.layer;
		firedProjectile.Launch(firingAngle);
		bulletsLeftInMagazine--;
		
		yield return StartCoroutine(DelayBetweenShots());
		isFiring = false;
	}
	
	protected IEnumerator DelayBetweenShots() 
	{
		Debug.Log("delaying");
		yield return new WaitForSeconds(delayBetweenShots);
	}
}
