using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
	[ColorUsage(true, true)]
	[SerializeField] private Color flashColor = Color.white;
	[SerializeField] private float flashTime = 0.3f;
	[SerializeField] private AnimationCurve flashSpeedCurve;
	
	private SpriteRenderer[] spriteRenderers;
	private Material[] materials;
	
	private Coroutine _damageFlashCoroutine;
	
	private void Awake() 
	{
		SpriteRenderer parentSpriteRenderer = GetComponent<SpriteRenderer>();
		SpriteRenderer[] childrenSpriteRenderers = GetComponentsInChildren<SpriteRenderer>();
		spriteRenderers = new SpriteRenderer[childrenSpriteRenderers.Length + 1];
		spriteRenderers[0] = parentSpriteRenderer;
		
		for (int i = 0; i < childrenSpriteRenderers.Length; i++) 
		{
			spriteRenderers[i+1] = childrenSpriteRenderers[i];
		}
		
		Init();
	}
	
	private void Init() 
	{
		materials = new Material[spriteRenderers.Length];
		
		for (int i = 0; i < spriteRenderers.Length; i++) 
		{
			materials[i] = spriteRenderers[i].material;
		}
	}
	
	public void CallDamageFlash()
	{
		_damageFlashCoroutine = StartCoroutine(DamageFlasher());
	}
	
	private IEnumerator DamageFlasher() 
	{
		// Set the color
		SetFlashColor();
		
		// Lerp the flash amount
		float currentFlashAmount = 0f;
		float elapsedTime = 0f;
		Debug.Log("flashing");
		while (elapsedTime < flashTime) 
		{
			// Iterate elapsedTime
			elapsedTime += Time.deltaTime;
			
			// Lerp the flash amount
			currentFlashAmount = Mathf.Lerp(1f, flashSpeedCurve.Evaluate(elapsedTime), (elapsedTime / flashTime));
			SetFlashAmount(currentFlashAmount);
			
			yield return null;
		}
	}
	
	private void SetFlashColor() 
	{
		// Set the color
		for (int i = 0; i < materials.Length; i++) 
		{
			materials[i].SetColor("_FlashColor", flashColor);
		}
	}
	
	private void SetFlashAmount(float amount) 
	{
		// Set the flash amount
		for (int i = 0; i < materials.Length; i++) 
		{
			materials[i].SetFloat("_FlashAmount", amount);
		}
	}
}
