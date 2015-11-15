using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EntityBase : MonoBehaviour {

	public Vec2i position;
	public Dir facing;
	public bool isMoving = false;
	public bool willObstruct = false;
	public bool willCollide = false;

	protected Map map;
	protected GameController gameController;
	
	public void Init(Vec2i position) {
		gameController = GameObject.Find ("GameController").GetComponent<GameController> ();
		map = GameObject.Find ("Map").GetComponent<Map> ();
		map.Init ();

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

	public virtual void Action() {
		
	}

	public virtual void OnCollision(EntityBase entity) {
		
	}

	protected void Face(Dir d) {
		facing = d;
	}

	protected void Move(Dir direction) {
		Move (position + direction.ToVec());
	}

	void Move(Vec2i destination) {

		facing = (destination - position).ToDir();
		
		bool isAdjacent = destination.IsAdjacent(position);
		if (!isAdjacent) {
			return;
		}

		List<EntityBase> entities = gameController.GetOccupants(destination);
		bool HitSomething = gameController.IsOccupied (destination);
		// entities will be null if colliding with a wall
		bool HitWall = (entities == null) && HitSomething;

		if ((!this.willObstruct && !HitWall) || !gameController.IsObstructed(destination)) {
			// Move immediately if valid
			// Must use this fn to change position.
			gameController.entityMap.ChangePosition (this, destination);

			Vector3 initialPosition = gameObject.transform.position;
			Vector3 targetPosition = map.GridToWorld (destination.x, destination.y);
			StartCoroutine (MoveTransform (initialPosition, targetPosition));
		}

		if (HitSomething) {

			if (HitWall) {
				this.OnCollision(null);
			} else {
				// TODO: This is fragile and may cause conflicts. May need to review how this is done.

				// Call OnCollision for current entity
				for(int i=0 ; i<entities.Count ; i++) {
					this.OnCollision(entities[i]);
				}

				// Call OnCollision for collided entities
				entities = gameController.GetOccupants(destination);
				if (entities != null) {
					for(int i=0 ; i<entities.Count ; i++) {
						entities[i].OnCollision(this);
					}
				}
			}
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

	public Team GetTeam() {
		Team team = GetComponent<Team> ();
		if (team) {
			return team;
		} else {
			team = gameObject.AddComponent<Team>();
			return team;
		} 
	}

}
