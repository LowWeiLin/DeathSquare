using UnityEngine;
using System.Collections;

/**
 *  Keeps track of what a player is currently equppied with.
 * 	Define slots for weapon, shield, etc.
 *  TODO: decide what to render over player?
 **/
[RequireComponent (typeof(PlayerBase))]
[RequireComponent (typeof(Inventory))]
public class Equipments : MonoBehaviour {

	PlayerBase player;
	Inventory inventory;

	
	// Equipment slots
	Equipable weapon = null;
	

	void Start() {
		Init ();
	}

	public void Init() {
		player = GetComponent<PlayerBase> ();
		inventory = GetComponent<Inventory> ();
	}


	// APIs

	void UnequipWeapon() {
		// Put back into inventory
		weapon = null;
	}

	void EquipWeapon(Weapon w) {
		Equipable e = w.gameObject.GetComponent<Equipable>();
		if (e == null) {
			return;
		}
		// Find and remove from inventory
		weapon = e;

	}

	void bool CanEquip(Equipable e) {
		if () {
		}

		return true;
	}


}
