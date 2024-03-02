using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
	[SerializeField] private Color flashColor = Color.white;
	[SerializeField] private float flashTime = - 0.25f;
	
	private SpriteRenderer[] spriteRenderers;
	private Material[] materials;
	
	private void Awake() 
	{
		SpriteRenderer parentSpriteRenderer = GetComponent<spriteRenderers>();
		SpriteRenderer[] childrenSpriteRenderers = GetComponentsInChildren<SpriteRenderer>();
		spriteRenderers = new SpriteRenderer[childrenSpriteRenderers.length + 1];
		spriteRenderers[0] = parentSpriteRenderer;
		
		for (int i = 0; i < childrenSpriteRenderers.length; i++) 
		{
			spriteRenderers[i+1] = childrenSpriteRenderers[i];
		}
		
		Init();
	}
	
	private void Init() 
	{
		materials = new Material[spriteRenderers.length];
		
		for (int i = 0; i < spriteRenderers.length; i++) 
		{
			materials[i] = spriteRenderers[i].material;
		}
	}
	
	private IEnumerator 
}
