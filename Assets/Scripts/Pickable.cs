using UnityEngine;
using System.Collections;

[RequireComponent (typeof(EntityBase))]
public class Pickable : MonoBehaviour {

	public EntityBase entity;
	public PlayerBase owner;

	void Start () {
		Init ();
	}

	private bool initialized = false;
	public void Init() {
		if (initialized)
			return;
		initialized = true;

		entity = GetComponent<EntityBase> ();
		owner = null;
	}

	
	// =====================================
	// 		APIs
	// =====================================

	public void Pick(PlayerBase picker) {
		if (CanBePicked (picker)) {
			// Add to picker's inventory
			EnterInventory(picker);
		}
	}

	
	public void Drop(PlayerBase dropper) {
		if (CanBeDropped(dropper)) {
			LeaveInventory(dropper);
		}
	}


	public bool CanBePicked(PlayerBase picker) {
		// Check if already picked
		if (owner != null) {
			return false;
		}

		// Check if in same location
		if (entity.position != picker.position) {
			return false;
		}

		// Picker needs an inventory
		if (picker.gameObject.GetComponent<Inventory>() == null) {
			return false;
		}

		// Must be able to add to inventory
		if (!picker.gameObject.GetComponent<Inventory>().CanAdd(this)) {
			return false;
		}

		return true;
	}

	public bool CanBeDropped(PlayerBase dropper) {
		// Not already picked up, cannot drop!
		if (owner == null) {
			return false;
		}

		// Only owner can drop!
		if (dropper != owner) {
			return false;
		}

		// Dropper needs an inventory
		if (dropper.gameObject.GetComponent<Inventory>() == null) {
			return false;
		}
		
		// Must be able to remove from inventory
		if (!dropper.gameObject.GetComponent<Inventory>().CanRemove(this)) {
			return false;
		}

		return true;
	}

	// =====================================
	// 		Internal functions
	// =====================================

	protected void MakeChild(PlayerBase player) {
		entity.gameController.entityMap.RemoveEntity (entity);
		transform.parent = player.gameObject.transform;
		transform.localPosition = Vector3.zero;
	}

	protected void EnterInventory(PlayerBase player) {
		// Add to picker's gameObject
		MakeChild(player);
		// Add to inventory
		player.GetComponent<Inventory> ().Add (this);
		// Set owner
		owner = player;
	}

	protected void MakeRoot(PlayerBase player) {
		entity.gameController.entityMap.AddEntity (entity);
		transform.parent = null;
		entity.SetPosition (player.position);
	}

	protected void LeaveInventory(PlayerBase dropper) {
		if (dropper != owner) {
			throw new UnityException("Cannot drop another players' item!");
		}

		// Add to map
		MakeRoot(owner);
		// Remove from inventory
		owner.GetComponent<Inventory> ().Remove (this);
		// Set owner
		owner = null;
	}
}
