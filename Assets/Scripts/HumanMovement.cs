using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Movement))]
public class HumanMovement : MonoBehaviour {

	Movement movement;

	void Start () {
		movement = GetComponent<Movement>();
	}

	void Update () {

		float dx = Input.GetAxisRaw("Horizontal");
		float dy = Input.GetAxisRaw("Vertical");

		float speed;
		if (dx != 0 && dy != 0) {
			speed = 2.0f;
		} else {
			speed = 3.0f;
		}

		movement.Move(dx, dy, speed);
	}
}
