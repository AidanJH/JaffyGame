using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	public GameObject enemyPrefab;
	public int numberOfEnemiesToSpawn = 20;
	public float spawnRadius = 50f;
	public float spawnInterval = 1f;

	void Start()
	{
		StartCoroutine(SpawnEnemiesRoutine());
	}

	IEnumerator SpawnEnemiesRoutine()
	{
		Transform player = GameObject.FindGameObjectWithTag("Player").transform;

		if (player == null)
		{
			Debug.LogError("Player not found");
			yield break;
		}

		for (int i = 0; i < numberOfEnemiesToSpawn; i++)
		{
			float randomAngle = Random.Range(0f, 360f);
			Vector3 spawnPosition = player.position + Quaternion.Euler(0f, 0f, randomAngle) * Vector3.right * spawnRadius;

			GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

			yield return new WaitForSeconds(spawnInterval);
		}
	}
}