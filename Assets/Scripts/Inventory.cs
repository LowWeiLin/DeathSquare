using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof(PlayerBase))]
public class Inventory : MonoBehaviour {

	PlayerBase player;

	public List<Pickable> items;
	private int itemLimit = 10;

	void Start () {
		Init ();
	}

	public void Init() {
		player = GetComponent<PlayerBase> ();
		items = new List<Pickable> ();
	}

	public bool CanAdd(Pickable item) {
		return items.Count < itemLimit;
	}

	public void Add(Pickable item) {
		if (CanAdd(item)) {
			items.Add(item);
		}
	}

	public bool CanRemove(Pickable item) {
		return items.Contains (item);
	}

	public void Remove(Pickable item) {
		if (CanRemove(item)) {
			items.Remove(item);
		}
	}

	// Functions to search inventory

	public List<Pickable> GetItems() {
		return new List<Pickable>(items);
	}

}
