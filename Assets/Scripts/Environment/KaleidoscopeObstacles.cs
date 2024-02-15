using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KaleidoscopeObstacles : MonoBehaviour
{
	[SerializeField] private GameObject baseShapePrefab;
	[SerializeField] private int numSegments = 6;
	[SerializeField] private float animationSpeed = 1.0f;
	[SerializeField] private float minScale = 1.3f;
	[SerializeField] private float maxScale = 1.5f;
	[SerializeField] private float scaleSpeed = 1.0f;
	
	private void Start() 
	{
		CreateKaleidoscopeObstacles();
	}
	
	private void Update() 
	{
		transform.Rotate(Vector3.forward, animationSpeed * Time.deltaTime);
		
		foreach (Transform child in transform)
		{
			// Calculate the normalized time for easing function
			float t = Mathf.PingPong(Time.time * scaleSpeed, 1.0f);
			// Calculate the eased scale using an ease-in-out function
			float easedScale = Mathf.Lerp(minScale, maxScale, EaseInOut(t));
			child.localScale = new Vector3(easedScale, easedScale, easedScale);
		}
	}
	
	private void CreateKaleidoscopeObstacles() 
	{
		for (int i = 0; i < numSegments; i++) 
		{
			float angle = i * (360f / numSegments);
			GameObject shape = Instantiate(baseShapePrefab, transform);
			shape.transform.RotateAround(transform.position, Vector3.forward, angle);
		}
	} 
	
	// Ease-in-out function for smoother scale animation
    private float EaseInOut(float t)
    {
        return t < 0.5f ? 2 * t * t : -1 + (4 - 2 * t) * t;
    }
}
