using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
	public float moveSpeed = 5f;
	public float rotationSpeed = 3f;
	protected Transform player;
	public float health = 100f;

	protected virtual void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;

		if (player == null)
		{
			Debug.LogError("Player not found!");
		}
	}

	protected virtual void Update()
	{
		
	}
}