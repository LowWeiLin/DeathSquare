using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/**
 * 
 **/
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

	public bool CanAdd() {
		return items.Count < itemLimit;
	}

	// Functions to search inventory


}
