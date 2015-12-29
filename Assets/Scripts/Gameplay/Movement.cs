using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Visuals))]
[RequireComponent(typeof(SteeringBasics))]
[RequireComponent(typeof(FollowPath))]
[RequireComponent(typeof(Wander1))]
public class Movement : MonoBehaviour {

	[HideInInspector]
	public GameController controller;

	SteeringBasics steeringBasics;
	FollowPath followPath;
	Wander1 wander1;

	public float speed = 1f;

	bool paused = false;

	void Start () {
		steeringBasics = GetComponent<SteeringBasics> ();
		followPath = GetComponent<FollowPath> ();
		wander1 = GetComponent<Wander1> ();

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
		RouteTowards(target.transform.position, range, speed);
	}

	public void Wander() {
		Vector3 accel = wander1.getSteering();
		steeringBasics.steer(accel);
		steeringBasics.lookWhereYoureGoing();
	}

	public void Stop() {
		this.GetComponent<Rigidbody> ().velocity = Vector3.zero;
		this.GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;
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
				if (path[0].EucledianDistance(origin) < 0.5f) {
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
		
		if (path.Count <= 1 || Vector3.Distance(transform.position, target) < 0.5f) {
			MoveTo(target);
		} else {
			
			LinePath linePath = Vec2iToLinePath (path);
			Vector3 accel = followPath.getSteering(linePath);
			steeringBasics.steer(accel);
			steeringBasics.lookWhereYoureGoing();
		}

	}

	LinePath Vec2iToLinePath(List<Vec2i> path) {
		Vector3[] vec3Array = new Vector3[path.Count];
		for (int i=0 ; i<path.Count ; i++) {
			vec3Array[i] = path[i].ToVec3();
		}
		return new LinePath(vec3Array);
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
