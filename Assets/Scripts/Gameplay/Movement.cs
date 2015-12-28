using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Visuals), typeof(Facing))]
public class Movement : MonoBehaviour {
	
	public GameController controller;

	public float speed = 1f;

	Rigidbody r;
	Facing facing;

	bool paused = false;

	void Start () {
		facing = GetComponent<Facing>();
		r = GetComponent<Rigidbody>();
		controller = GameController.Instance;
		controller.Init ();
	}

	public void MoveTowards(Maybe<GameObject> target, float speed=float.MaxValue) {
		target.IfPresent(t => {
			Vector3 direction = t.transform.position - transform.position;
			MoveTowards (direction, speed);
		});
	}

	public void MoveTowards(Vector3 direction, float speed=float.MaxValue) {
		direction.Normalize();
		Move(direction.x, direction.z, speed);
	}

	public void RouteTowards(GameObject target, float range=0.1f, float speed=float.MaxValue) {
		RouteTowards(target.transform.position, range, speed);
	}

	private static Board board;
	private List<Vec2i> path;
	private Vec2i pathOrigin;
	private Vec2i pathGoal;
	public void RouteTowards(Vector3 target, float range=0.1f, float speed=float.MaxValue) {
		Vec2i origin = controller.map.WorldToGrid (transform.position);
		Vec2i goal = controller.map.WorldToGrid (target);

		// Stop route if close enough to goal
		if ((transform.position - target).magnitude <= range) {
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
				//Debug.Log("recalculating path");
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
		if (direction == Vector3.zero) {
			direction = target - transform.position;
		}

		MoveTowards (direction, speed);
	}

	public void Move(float dx, float dy, float speed=float.MaxValue) {
		if (paused) {
			return;
		}

		if (speed == float.MaxValue) {
			speed = this.speed;
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
