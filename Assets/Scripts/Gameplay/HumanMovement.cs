﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Movement))]
public class HumanMovement : MonoBehaviour {

	Movement movement;
	Attack attack;

	Vector3 routeDestination = Vector3.down;
	const float routePrecision = 0.3f;
	GameObject target;

	private LayerMask floorLayerMask;
	private LayerMask unitsLayerMask;
	public GameObject movementEffect;
	
	void Start () {
		floorLayerMask = 1 << LayerMask.NameToLayer ("Floor"); // only check for collisions with this layer
		unitsLayerMask = 1 << LayerMask.NameToLayer ("Units"); // only check for collisions with this layer
		movement = GetComponent<Movement>();
		attack = GetComponent<Attack>();
	}

	void Update () {


		if( Input.GetMouseButtonDown(0) )
		{
			Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
			RaycastHit hit;

			// Click on floor to route towards point.
			if( Physics.Raycast( ray, out hit, 100, floorLayerMask ) )
			{
				Instantiate(movementEffect, hit.point + movementEffect.transform.localPosition, Quaternion.identity);

				// Location is unobstructed
				if (movement.controller.IsUnobstructed(movement.controller.map.WorldToGrid(hit.point))) {
					// Instant teleport
					//transform.position = hit.point;
					routeDestination = hit.point;
					target = null;
				}
			}

			// Click on unit to move to unit, attack if enemy.
			if( Physics.Raycast( ray, out hit, 100, unitsLayerMask ) )
			{
				// Is not self
				if (hit.transform.gameObject != this.gameObject) {
					routeDestination = Vector3.down;
					target = hit.transform.gameObject;
				}
			}
		}
		
		if (routeDestination != Vector3.down) {
			if (Vector3.Distance(transform.position, routeDestination) < routePrecision) {
				routeDestination = Vector3.down;
				movement.Stop();
			}
			movement.RouteTowards (routeDestination, routePrecision);
		}

		if (target != null) {
			if (Vector3.Distance(transform.position, target.transform.position) < attack.range) {
				Team team = target.GetComponent<Team>();
				if (this.GetComponent<Team>().IsEnemy(team)) {
					attack.Hit(target);
				} else {
					movement.RouteTowards (target, routePrecision);
				}
			} else {
				movement.RouteTowards (target, routePrecision);
			}
		}
	}
}
