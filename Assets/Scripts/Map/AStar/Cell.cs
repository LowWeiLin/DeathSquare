using UnityEngine;
using System.Collections;

public class Cell {
	public Vector2 coordinates {get; set;}
	public bool walkable = false;
	public virtual bool IsWalkable() {
		return walkable;
	}
	public virtual float MovementCost() {
		return 1;
	}
}