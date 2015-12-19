using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	GameObject model;

	void Start () {
		model = transform.GetChild(0).gameObject;
	}

	void Update () {

		float dx = Input.GetAxisRaw("Horizontal");
		float dy = Input.GetAxisRaw("Vertical");

		float speed;
		if (dx != 0 && dy != 0) {
			speed = 1.0f;
		} else {
			speed = 1.5f;
		}

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
