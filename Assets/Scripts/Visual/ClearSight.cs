﻿using UnityEngine;
using System.Collections;


public class ClearSight : MonoBehaviour
{
	public float DistanceToPlayer = 10.0f;

	public LayerMask wallsLayerMask = -1;

	void Start () {
		wallsLayerMask = 1 << LayerMask.NameToLayer ("Walls"); // only check for collisions with this layer
	}

	void Update()
	{
		RaycastHit[] hits;
		// you can also use CapsuleCastAll()
		// TODO: setup your layermask it improve performance and filter your hits.
		hits = Physics.RaycastAll(transform.position, transform.forward, DistanceToPlayer, wallsLayerMask.value);
		foreach(RaycastHit hit in hits)
		{
			Renderer R = hit.collider.GetComponent<Renderer>();
			if (R == null)
				continue; // no renderer attached? go to next hit
			// TODO: maybe implement here a check for GOs that should not be affected like the player
			
			
			AutoTransparent AT = R.GetComponent<AutoTransparent>();
			if (AT == null) // if no script is attached, attach one
			{
				AT = R.gameObject.AddComponent<AutoTransparent>();
			}
			AT.BeTransparent(); // get called every frame to reset the falloff
		}
	}
}