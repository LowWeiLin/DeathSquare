using UnityEngine;
using System.Collections;

public class FacingIndicator : MonoBehaviour {

	public Sprite[] sprites;

	SpriteRenderer spriteRenderer;
	EntityBase entity;


	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
		entity = GetComponentInParent<EntityBase> ();
		Debug.Assert (spriteRenderer != null);
		Debug.Assert (entity != null);
	}
	
	// Update is called once per frame
	void Update () {
		spriteRenderer.sprite = sprites [(int)entity.facing];
	}
}
