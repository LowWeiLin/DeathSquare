using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Movement : MonoBehaviour {

	GameObject model;
	Rigidbody r;

	float collisionThreshold = 1.2f;
	GameController controller;

	void Start () {
		model = transform.GetChild(0).gameObject;
		r = GetComponent<Rigidbody>();
		controller = GameObject.Find("GameController").GetComponent<GameController>();
		controller.Init ();
	}

	public void MoveTowards(GameObject target, float speed) {
		Vector3 direction = target.transform.position - transform.position;
		MoveTowards (direction, speed);
	}

	public void MoveTowards(Vector3 direction, float speed) {
		//if (direction.sqrMagnitude > collisionThreshold) {
			direction.Normalize();
			Move(direction.x, direction.z, speed);
		//}
	}


	public void RouteTowards(GameObject target) {
		Vec2i origin = controller.map.WorldToGrid (transform.position);
		Vec2i goal =  controller.map.WorldToGrid (target.transform.position);

		if ((transform.position - target.transform.position).magnitude <= 1.5f)
			return;

		Board board = new Board ();
		board.CreateBoard (controller.map.map, controller.map.width, controller.map.height);
		List<Vec2i> path = board.FindPathVec (origin, goal);

		Vector3 direction = Vector3.zero;

		// Draw path
		for (int i=0; i<path.Count-1; i++) {
			Debug.DrawLine(path[i].ToVec3(), path[i+1].ToVec3(), Color.black);
		}

		for (int i=0 ; i<path.Count ; i++) {
			direction += (path[i].ToVec3() - transform.position)/((i+1)*(i+1));
			if (i>=0)
				break;
		}

		direction *= 2;

		Debug.Log (origin + " -> " + goal);
		Debug.Log (direction);

		Debug.DrawRay (origin.ToVec3 (), direction);

		MoveTowards (direction, 3f);
	}

	public void Move(float dx, float dy, float speed) {

		Vector3 offset = (Vector3.right * dx + Vector3.forward * dy) * Time.deltaTime * speed;
		if (r == null) {
			transform.Translate(offset);
		} else {
			r.MovePosition(offset + transform.position);
		}

		Vector3 moveDirection = new Vector3(dx, 0, dy);

		if (moveDirection != Vector3.zero) {
			Quaternion newRotation = Quaternion.LookRotation(-moveDirection);

			// HACK compensate for initial rotation of model
			newRotation *= Quaternion.Euler(270, 0, 0);

			model.transform.rotation = Quaternion.Slerp(model.transform.rotation, newRotation, Time.deltaTime * 8);
		}
	}
}
