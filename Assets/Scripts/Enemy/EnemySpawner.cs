using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	public GameObject enemyPrefab;
	public int numberOfEnemiesToSpawn = 20;
	public float spawnRadius = 10f;
	
	void Start() 
	{
		SpawnEnemies();
	}
	
	private void SpawnEnemies() 
	{
		Transform player = GameObject.FindGameObjectWithTag("Player").transform;
		
		if (player == null) 
		{
			Debug.LogError("Player not found");
			return;
		}
		
		for (int i = 0; i < numberOfEnemiesToSpawn; i++) 
		{
			float randomAngle = Random.Range(0f, 360f);
			Vector3 spawnPosition = player.position + Quaternion.Euler(0f, 0f, randomAngle) * Vector3.right * spawnRadius;
			GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
		}
	}
}
