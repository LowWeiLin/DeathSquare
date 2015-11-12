using UnityEngine;
using System.Collections;

public struct Vec2i {
	public readonly int x;
	public readonly int y;

	public Vec2i(int x, int y) {
		this.x = x;
		this.y = y;
	}

	public override int GetHashCode() {
		return x * 10000 + y;	
	}

	public bool isAdjacent(Vec2i v) {
		if (Mathf.Abs(v.x - x) <= 1 || Mathf.Abs(v.y - y) <= 1) {
			return true;
		}
		return false;
	}
	
	public static Vec2i operator +(Vec2i left, Vec2i right) {
		return new Vec2i(right.x + left.x, right.y + left.y);
	}
	
	public static Vec2i operator -(Vec2i left, Vec2i right) {
		return new Vec2i(right.x - left.x, right.y - left.y);
	}
}

public class EntityBase : MonoBehaviour {

	public Vec2i position;
	public Vec2i moveToPosition; // UDLR of position.
	public bool isMoving = false;
	public bool isCollider = false;

	protected Map map;
	protected GameController gameController;

	private Vector3 initialPosition;
	private Vector3 targetPosition;
	private float epi = 0.01f;
	float progress;

	// Use this for initialization
	protected void Start () {
		gameController = GameObject.Find ("GameController").GetComponent<GameController> ();
		map = GameObject.Find ("Map").GetComponent<Map> ();

		this.transform.position = map.GridToWorld (position);

	}
	
	// Update is called once per frame
	void Update () {
		if (isMoving && progress <= 1f) {

			transform.position = Vector3.Lerp (initialPosition, targetPosition, progress);
			if ((transform.position - targetPosition).magnitude <= epi) {
				isMoving = false;
				progress = 0f;
				transform.position = targetPosition;
			} else {
				progress += 0.05f;
			}
		}
	}
	
	public Vec2i getPosition() {
		return position;
	}

	public bool IsOccupied(Vec2i position) {
		return gameController.entityMap.isOccupied(position);
	}

	public void setMoveToPosition(Vec2i pos) {
		// Check isAdjacent, no obstacles on map and entity list
		if (pos.isAdjacent (position) && !gameController.isOccupied(pos)) {
		
			this.moveToPosition = position;

			// Move immediately if valid
			// Must use this fn to change position.
			gameController.entityMap.ChangePosition(this, pos);

			this.initialPosition = gameObject.transform.position;
			this.targetPosition = map.GridToWorld (pos.x, pos.y);

			this.isMoving = true;
		}

	}

}
