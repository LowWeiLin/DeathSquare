using UnityEngine;
using System.Collections;

public class EntityBase : MonoBehaviour {

	public Vec2i position;
	public Dir facing;
	public bool isMoving = false;
	public bool isCollider = false;

	protected Map map;
	protected GameController gameController;
	
	public void Init(Vec2i position) {
		gameController = GameObject.Find ("GameController").GetComponent<GameController> ();
		map = GameObject.Find ("Map").GetComponent<Map> ();

		this.position = position;
		transform.position = map.GridToWorld (position);

		facing = Dir.Up;

		gameController.RegisterEntity (this);
	}

	public void DestroyEntity() {
		gameController.UnregisterEntity (this);
		Destroy (this.gameObject);
	}

	public virtual bool CanAct() {
		return !isMoving;
	}

	public virtual void Action () {
		
	}

	public virtual void OnCollision(EntityBase entity) {
		
	}

	public void Move(Dir direction) {
		Move (position + direction.ToVec());
	}
	
	public void Move(Vec2i pos) {
		// Check isAdjacent, no obstacles on map and entity list
		if (pos.IsAdjacent (position) && !gameController.IsOccupied (pos)) {
		
			// Move immediately if valid
			// Must use this fn to change position.
			gameController.entityMap.ChangePosition (this, pos);

			Vector3 initialPosition = gameObject.transform.position;
			Vector3 targetPosition = map.GridToWorld (pos.x, pos.y);

			facing = map.WorldToGrid((initialPosition - targetPosition)).ToDir();

			StartCoroutine (MoveTransform (initialPosition, targetPosition));
		} else if (gameController.IsOccupied (pos)) {
			EntityBase e = gameController.GetOccupant(pos);

			// Call OnCollision functions
			OnCollision(e);

		}
	}

	IEnumerator MoveTransform(Vector3 initialPosition, Vector3 targetPosition) {
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
