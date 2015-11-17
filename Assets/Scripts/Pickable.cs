using UnityEngine;
using System.Collections;

[RequireComponent (typeof(EntityBase))]
public class Pickable : MonoBehaviour {

	EntityBase entity;
	PlayerBase owner;

	void Start () {
		Init ();
	}
	
	public void Init() {
		entity = GetComponent<EntityBase> ();
		owner = null;
	}


	// APIs
	public void Pick(PlayerBase picker) {
		if (CanBePicked (picker)) {
			// Add to picker's inventory
			MakeChild(picker);

			// Set owner
			owner = picker;
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

		return true;
	}

	public void Drop(PlayerBase dropper) {
		if (CanBeDropped(dropper)) {

		}
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

		return true;
	}

	protected void MakeChild(PlayerBase player) {
		entity.gameController.entityMap.RemoveEntity (entity);
		transform.parent = player.gameObject.transform;
		transform.localPosition = Vector3.zero;
	}

}
