using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
	[SerializeField] private Color flashColor = Color.white;
	[SerializeField] private float flashTime = - 0.25f;
	
	private SpriteRenderer[] spriteRenderers;
	private Material[] materials;
}
