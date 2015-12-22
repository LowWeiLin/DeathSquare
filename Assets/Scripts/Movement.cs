using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Visuals))]
public class Movement : MonoBehaviour {

	GameObject model;
	Rigidbody r;
	GameController controller;

	float collisionThreshold = 1.2f;
	bool paused = false;

	void Start () {
		model = GetComponent<Visuals>().model;
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
		Vec2i goal = controller.map.WorldToGrid (target.transform.position);

		if ((transform.position - target.transform.position).magnitude <= 1.5f) {
			return;
		}

		Board board = new Board ();
		board.CreateBoard (controller.map.map, controller.map.width, controller.map.height);
		List<Vec2i> path = board.FindPathVec (origin, goal);

		Vector3 direction = Vector3.zero;

		// Draw path
		for (int i=0; i<path.Count-1; i++) {
			Debug.DrawLine(path[i].ToVec3(), path[i+1].ToVec3(), Color.black);
		}

		// Smooth path
		List<Vector3> smoothPath = new List<Vector3> ();
		for (int i=0; i<path.Count-1; i++) {
			if (i == 0) {
				smoothPath.Add(transform.position);
			} else if (i == path.Count-1) {
				smoothPath.Add(goal.ToVec3());
			} else {
				smoothPath.Add((smoothPath[i-1] + 2*path[i].ToVec3() + path[i+1].ToVec3())/4.0f);
			}
		}
		for (int i=0; i<path.Count-2; i++) {
			Debug.DrawLine(smoothPath[i], smoothPath[i+1], Color.red);
		}


		for (int i=0 ; i<smoothPath.Count ; i++) {
			direction += (smoothPath[i] - transform.position)/((i+1)*(i+1));
			if (i>=1)
				break;
		}


		Debug.DrawRay (origin.ToVec3 (), direction);

		MoveTowards (direction, 3f);
	}

	public void Move(float dx, float dy, float speed) {

		if (paused) {
			return;
		}

		Vector3 offset = (Vector3.right * dx + Vector3.forward * dy) * Time.deltaTime * speed;
		if (r == null) {
			transform.Translate(offset);
		} else {
			r.MovePosition(offset + transform.position);
		}

		Vector3 moveDirection = new Vector3(dx, 0, dy);
		Face(moveDirection);
	}

	public void Face(Vector3 direction) {
		if (direction == Vector3.zero) {
			return;
		}
		Quaternion newRotation = Quaternion.LookRotation(-direction);

		// HACK compensate for initial rotation of model
		newRotation *= Quaternion.Euler(270, 0, 0);

		model.transform.rotation = Quaternion.Slerp(model.transform.rotation, newRotation, Time.deltaTime * 8);
	}

	public void Pause() {
		paused = true;
	}

	public void Unpause() {
		paused = false;
	}
}
