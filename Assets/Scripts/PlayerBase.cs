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

	public void Fire() {
		GameObject shot = Instantiate(projectile) as GameObject;
		shot.GetComponent<Projectile>().Init(position, facing);
	}
}
