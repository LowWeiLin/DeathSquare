﻿using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	GameObject model;
	float collisionThreshold = 1.2f;

	void Start () {
		model = transform.GetChild(0).gameObject;
	}

	public void MoveTowards(GameObject target, float speed) {
		Vector3 direction = target.transform.position - transform.position;
		Debug.Log(direction.sqrMagnitude);
		if (direction.sqrMagnitude > collisionThreshold) {
			direction.Normalize();
			Move(direction.x, direction.z, speed);
		}
	}

	public void Move(float dx, float dy, float speed) {
		// TODO when up against a wall, stop moving
		transform.Translate(Vector3.right * dx * Time.deltaTime * speed);
		transform.Translate(Vector3.forward * dy * Time.deltaTime * speed);

		Vector3 moveDirection = new Vector3(dx, 0, dy);

		if (moveDirection != Vector3.zero) {
			Quaternion newRotation = Quaternion.LookRotation(-moveDirection);

			// HACK compensate for initial rotation of model
			newRotation *= Quaternion.Euler(270, 0, 0);

			model.transform.rotation = Quaternion.Slerp(model.transform.rotation, newRotation, Time.deltaTime * 8);
		}
	}
}