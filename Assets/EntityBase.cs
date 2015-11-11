using UnityEngine;
using System.Collections;

public struct Vec2i {
	public int x;
	public int y;

	public override int GetHashCode() {
		return x * 10000 + y;	
	}
}

public class EntityBase : MonoBehaviour {

	private Vec2i position;
	private Vec2i moveToPosition; // UDLR of position.
	private bool isMoving = false;
	private bool isCollider = false;

	protected MapGenerator mapGenerator;

	// Use this for initialization
	void Start () {
		mapGenerator = GameObject.Find ("Map").GetComponent<MapGenerator> ();
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	public Vec2i getPosition() {
		return position;
	}

	public void setMoveToPosition(Vec2i pos) {
		// Check valid pos
		this.moveToPosition = pos;

		this.isMoving = true;

	}

}
