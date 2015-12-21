using UnityEngine;
using System.Collections;

[RequireComponent (typeof(EntityBase))]
[RequireComponent (typeof(Pickable))]
public class Equipable : MonoBehaviour {
	
	
	EntityBase entity;
	Pickable pickable;
	
	string slot = "weapon";
	
	void Start () {
		Init ();
	}
	
	private bool initialized = false;
	public void Init() {
		if (initialized)
			return;
		initialized = true;
		
		entity = GetComponent<EntityBase> ();
		pickable = GetComponent<Pickable> ();
	}
	
	
	// =====================================
	// 		Equip/Unequip
	// =====================================
	
	public void Equip(string slot) {
		if (pickable.owner) {
			pickable.owner.GetComponent<Inventory>().Equip(this, slot);
		}
	}
	
	public void Unequip(string slot) {
		if (pickable.owner) {
			pickable.owner.GetComponent<Inventory>().Unequip(slot);
		}
	}
	
	
	// =====================================
	// 		Checks
	//		These may be overridden, depending on the equipment(?)
	// =====================================
	
	public bool CanEquip(string slot) {
		if (slot != this.slot)
			return false;
		
		// Needs an owner
		if (pickable.owner == null) {
			return false;
		}
		
		return true;
	}
	
	public bool CanUnequip(string slot) {
		return true;
	}
	
}