using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof(Inventory))]
public class PlayerBase : EntityBase {
	
	protected Inventory inventory;
	
	public new void Init(Vec2i position) {
		inventory = gameObject.GetComponent<Inventory> ();
		
		base.Init(position);

		gameController.RegisterPlayer (this);
		
	}
	
	// ===============================
	// 		APIs
	// ===============================
	
	
	// =====================================
	// 		Observation functions
	// =====================================

	
	
	// =====================================
	// 		Action functions
	// =====================================

	public void MoveTo() {
		
	}

	public void Attack() {
		Equipable weapon = inventory.GetEquip ("weapon");
		if (weapon != null) {
			weapon.gameObject.GetComponent<WeaponBase> ().Attack (this);
		}
	}
}