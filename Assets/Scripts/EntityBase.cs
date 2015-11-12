using UnityEngine;
using System.Collections;

public class EntityBase : MonoBehaviour {

	public Vec2i position;
	public Vec2i moveToPosition; // UDLR of position.
	public bool isMoving = false;
	public bool isCollider = false;

	protected Map map;
	protected GameController gameController;
	
	public void Init(Vec2i position) {
		gameController = GameObject.Find ("GameController").GetComponent<GameController> ();
		map = GameObject.Find ("Map").GetComponent<Map> ();

		this.position = position;
		transform.position = map.GridToWorld (position);

		gameController.RegisterEntity (this);
	}

	public virtual bool CanAct() {
		return !isMoving;
	}

	public virtual void Action () {
		
	}

	public void setMoveToPosition(Vec2i pos) {
		// Check isAdjacent, no obstacles on map and entity list
		if (pos.isAdjacent (position) && !gameController.IsOccupied(pos)) {
		
			this.moveToPosition = position;

			// Move immediately if valid
			// Must use this fn to change position.
			gameController.entityMap.ChangePosition(this, pos);

			Vector3 initialPosition = gameObject.transform.position;
			Vector3 targetPosition = map.GridToWorld (pos.x, pos.y);

			StartCoroutine(Move(initialPosition, targetPosition));
		}
	}

	IEnumerator Move(Vector3 initialPosition, Vector3 targetPosition) {
		isMoving = true;
		float progress = 0f;
		while (progress <= 1f) {
			transform.position = Vector3.Lerp (initialPosition, targetPosition, progress);
			progress += 0.05f;
			yield return null;
		}
		transform.position = targetPosition;
		isMoving = false;
	}

	// ===============================
	// 		Entity API
	// ===============================

	public bool IsOutOfBounds(Vec2i position) {
		return map.OutOfBounds(position);
	}
}
