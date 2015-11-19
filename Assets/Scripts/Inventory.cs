using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof(PlayerBase))]
public class Inventory : MonoBehaviour {

	PlayerBase player;

	public List<Pickable> items;
	private int itemLimit = 10;

	public Dictionary<string, Equipable> equipments;

	void Start () {
		Init ();
	}

	private bool initialized = false;
	public void Init() {
		if (initialized)
			return;
		initialized = true;

		player = GetComponent<PlayerBase> ();
		items = new List<Pickable> ();
		equipments = new Dictionary<string, Equipable>();
	}

	// =====================================
	// 		Add/Remove from inventory
	// =====================================

	public bool CanAdd(Pickable item) {
		return items.Count < itemLimit;
	}

	public bool CanRemove(Pickable item) {
		if (!IsInInventory(item)) {
			return false;
		}
		Equipable e = item.gameObject.GetComponent<Equipable> ();
		if (e != null) {
			if (IsEquipped(e)) {
				if (!CanUnequip(GetSlot(e))) {
					return false;
				}
			}
		}
		return true;
	}
	
	public void Add(Pickable item) {
		if (CanAdd(item)) {
			items.Add(item);
			item.gameObject.GetComponent<Renderer>().enabled = false;
		}
	}

	public void Remove(Pickable item) {
		if (CanRemove(item)) {
			Equipable e = item.gameObject.GetComponent<Equipable> ();
			if (e != null) {
				if (IsEquipped(e)) {
					Unequip(GetSlot(e));
				}
			}

			items.Remove(item);
			item.gameObject.GetComponent<Renderer>().enabled = true;
		}
	}

	// =====================================
	// 		Functions to search inventory
	// =====================================

	public List<Pickable> GetItems() {
		return new List<Pickable>(items);
	}

	public bool IsInInventory (Pickable p) {
		return items.Contains (p);
	}

	public bool IsEquipped (Equipable e) {
		foreach(Equipable eq in equipments.Values)
		{
			if(eq == e)
				return true;
		}
		return false;
	}

	public string GetSlot (Equipable e) {
		foreach(KeyValuePair<string, Equipable> eq in equipments) {
			if(eq.Value == e)
				return eq.Key;
		}
		return null;
	}

	public Equipable GetEquip(string slot) {
		Equipable e = null;
		if (equipments.TryGetValue(slot, out e)) {
			return e;
		}
		return null;
	}

	// =====================================
	// 		Add/Remove from equipment slots
	// =====================================


	public bool CanEquip(Equipable item, string slot) {
		// Check present in inventory
		if (!IsInInventory(item.gameObject.GetComponent<Pickable>())) {
			return false;
		}
		// Check equipment allows equip
		if (!item.CanEquip (slot)) {
			return false;
		}

		// If slot already has equip, must be able to unequip
		if (GetEquip(slot)) {
			if (!CanUnequip(slot)) {
				return false;
			}
		}

		return true;
	}
	
	public bool CanUnequip(string slot) {
		Equipable e = GetEquip (slot);
		// Must have something to unequip
		if (e == null) {
			return false;
		}

		// Check equipment allows unequip
		if (!e.CanUnequip (slot)) {
			return false;
		}
		return true;
	}
	
	public void Equip(Equipable e, string slot) {
		if (CanEquip(e, slot)) {
			// Unequip existing equipment in slot
			if (GetEquip(slot)) {
				Unequip(slot);
			}

			// Mark as equipped in slot
			equipments.Add(slot, e);

			// Render when equipped
			e.gameObject.GetComponent<Renderer>().enabled = true;
		}
	}
	
	public void Unequip(string slot) {
		Equipable e = GetEquip (slot);
		if (CanUnequip(slot)) {

			// Remove from slot
			equipments.Remove(slot);

			// Do not render when equipped
			e.gameObject.GetComponent<Renderer>().enabled = false;
		}
	}

}
