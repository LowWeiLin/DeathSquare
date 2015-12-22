using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Visuals), typeof(Facing))]
public class Movement : MonoBehaviour {

	GameObject model;
	Rigidbody r;
	GameController controller;
	Facing facing;

	float collisionThreshold = 1.2f;
	bool paused = false;

	void Start () {
		facing = GetComponent<Facing>();
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

	private static Board board;
	private List<Vec2i> path;
	private Vec2i pathOrigin;
	private Vec2i pathGoal;
	public void RouteTowards(GameObject target, float range=0.1f) {
		Vec2i origin = controller.map.WorldToGrid (transform.position);
		Vec2i goal = controller.map.WorldToGrid (target.transform.position);

		// Stop route if close enough to goal
		if ((transform.position - target.transform.position).magnitude <= range) {
			return;
		}

		// Initialize board
		if (board == null) {
			board = new Board ();
			board.CreateBoard (controller.map.map, controller.map.width, controller.map.height);
		}

		// Use previously found path if no change to origin and goal
		if (path != null && origin == pathOrigin && goal == pathGoal)
		{
		}
		// If goal did not change, can use previous path if following it
		// Or if goal did not change much as compared to distance left to travel.
		else if (path != null && (goal == pathGoal || 
		                          goal.ManhattanDistance(pathGoal) < 0.1f*path.Count))
		{
			// Remove path walked
			while (path.Count > 0) {
				if (path[0].ManhattanDistance(origin) < 2) {
					path.RemoveAt(0);
				} else {
					break;
				}
			}
			pathOrigin = origin;
			
			// Did not follow path, recalculate path.
			if (path.Count == 0) {
				Debug.Log("recalculating path");
				path = board.FindPathVec (origin, goal);
				pathOrigin = origin;
				pathGoal = goal;
			}
		}
		else
		{
			// Simply calculate path
			path = board.FindPathVec (origin, goal);
			pathOrigin = origin;
			pathGoal = goal;
		}

		Vector3 direction = Vector3.zero;

		// Draw path
		/*
		for (int i=0; i<path.Count-1; i++) {
			Debug.DrawLine(path[i].ToVec3(), path[i+1].ToVec3(), Color.black);
		}
		*/

		for (int i=0 ; i<path.Count ; i++) {
			direction += (path[i].ToVec3() - transform.position)/((i+1)*(i+1));
			if (i>=1)
				break;
		}

		//Debug.DrawLine(transform.position, transform.position+direction, Color.red);

		MoveTowards (direction, 1f);
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
		facing.Face(moveDirection);
	}

	public void Pause() {
		paused = true;
	}

	public void Unpause() {
		paused = false;
	}
}
