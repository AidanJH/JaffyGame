using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BurstWeapon : Weapon
{
	[SerializeField] private int bulletsPerBurst;
	[SerializeField] private float delayBetweenShots;

	public override void WeaponShoot(float firingAngle)
	{
		if (reloaded)
		{
			for (int i = 0; i < bulletsPerBurst; i++) 
			{
				Projectile firedProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
				firedProjectile.gameObject.layer = gameObject.layer;
				firedProjectile.Launch(firingAngle);
				
				yield WaitForSeconds(delayBetweenShots);
				// if (i < bulletsPerBurst - 1) 
				// {
				// 	StartCoroutine(DelayBetweenShots(i));
				// }
			}
			reloaded = false;
			StartCoroutine(ReloadWeapon());
		} else {
			Debug.Log("Weapon not reloaded yet");
		}
	}
	
	// private IEnumerator DelayBetweenShots(int index) 
	// {
	// 	yield return new WaitForSeconds(delayBetweenShots * (index + 1));
	// }
}
