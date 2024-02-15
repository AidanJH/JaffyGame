using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplittingEnemy : Enemy
{
	[SerializeField] private GameObject enemyPrefab;
	[SerializeField] private int maxSplits = 2;
	[SerializeField] private float minSizeToSplit = 0.5f;

	protected override void Rotate() {}

	protected override void OnCollisionEnter2D(Collision2D collision) 
	{
		if(collision.gameObject.layer == LayerMask.NameToLayer("PlayerWeapons") 
			|| collision.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
			Debug.Log("Player shot enemy: " + gameObject.name);
			health -= collisionDamage;
			if (health <= 0) 
			{
				// If the enemy is smaller than the minimum size, don't split anymore, just destroy
				if (transform.localScale.x > minSizeToSplit && transform.localScale.y > minSizeToSplit) 
				{
					Split(enemyPrefab);
				}
				Destroy(gameObject);  
			}
		} else {
			Debug.Log("The enemy has collided with" + collision.gameObject.name);
		}	
	}
	
	private void Split(GameObject originalEnemyPrefab) 
	{
		// Instantiate two enemies at the same position and make them half the size
		GameObject babyEnemy1 = Instantiate(originalEnemyPrefab, transform.position, Quaternion.identity);
		babyEnemy1.transform.localScale = transform.localScale * 0.5f;
		// Pass the enemy prefab to the new enemy
		babyEnemy1.GetComponent<SplittingEnemy>().enemyPrefab = originalEnemyPrefab;

		GameObject babyEnemy2 = Instantiate(originalEnemyPrefab, transform.position, Quaternion.identity);
		babyEnemy2.transform.localScale = transform.localScale * 0.5f;
		babyEnemy2.GetComponent<SplittingEnemy>().enemyPrefab = originalEnemyPrefab;
	}
}
