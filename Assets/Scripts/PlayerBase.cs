using UnityEngine;
using System.Collections;

public class PlayerBase : EntityBase {

	public GameObject projectile;
	
	public new void Init(Vec2i position) {
		base.Init(position);
		isCollider = true;
		gameController.RegisterPlayer (this);
	}

	// ===============================
	// 		API
	// ===============================

	public bool IsOccupied(Vec2i position) {
		return gameController.entityMap.IsOccupied(position);
	}

	public bool IsOccupied(Dir direction) {
		return IsOccupied(direction.ToVec() + position);
	}

	public void Attack() {
		foreach (Transform child in transform) {
			if (child.gameObject.GetComponent<Weapon>() != null) {
				child.gameObject.GetComponent<Weapon> ().Attack(this);
			}
		}
	}
}
