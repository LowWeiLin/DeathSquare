using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Visuals), typeof(SteeringBasics))]
public class Movement : MonoBehaviour {

	[HideInInspector]
	public GameController controller;

	SteeringBasics steeringBasics;

	public float speed = 1f;

	Rigidbody r;

	bool paused = false;

	void Start () {
		steeringBasics = GetComponent<SteeringBasics> ();
		r = GetComponent<Rigidbody>();
		controller = GameController.Instance;
		controller.Init ();
	}

	public void MoveTowards(Maybe<GameObject> target, float speed=float.MaxValue) {
		target.IfPresent(t => {
			MoveTo (t.transform.position, speed);
		});
	}

	public void MoveTo(Vector3 targetPosition, float speed=float.MaxValue) {
		Vector3 accel = steeringBasics.arrive(targetPosition);
		steeringBasics.steer(accel);
		steeringBasics.lookWhereYoureGoing();
	}

	public void RouteTowards(GameObject target, float range=0.1f, float speed=float.MaxValue) {
		MoveTo (target.transform.position, speed);
		//RouteTowards(target.transform.position, range, speed);
	}

	private static Board board;
	private List<Vec2i> path;
	private Vec2i pathOrigin;
	private Vec2i pathGoal;
	public void RouteTowards(Vector3 target, float range=0.1f, float speed=float.MaxValue) {
		MoveTo (target, speed);
		return;

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

		if (path.Count > 1)
			MoveTo (path[1].ToVec3(), speed);
	}

	/*
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
	}*/

	public void Pause() {
		paused = true;
	}

	public void Unpause() {
		paused = false;
	}
}
