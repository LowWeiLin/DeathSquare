using UnityEngine;
using System.Collections;

public struct Vec2i {
	public int x;
	public int y;

	public Vec2i(int x, int y) {
		this.x = x;
		this.y = y;
	}

	public override int GetHashCode() {
		return x * 10000 + y;	
	}
}

public class EntityBase : MonoBehaviour {

	public Vec2i position;
	public Vec2i moveToPosition; // UDLR of position.
	public bool isMoving = false;
	public bool isCollider = false;

	protected MapGenerator mapGenerator;
	protected GameController gameController;

	private Vector3 targetPosition;
	private float epi = 0.00001f;
	float progress;

	// Use this for initialization
	protected void Start () {
		gameController = GameObject.Find ("GameController").GetComponent<GameController> ();
		mapGenerator = GameObject.Find ("Map").GetComponent<MapGenerator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isMoving && progress <= 1f) {

			transform.position = Vector3.Lerp (transform.position, targetPosition, progress);
			if ((transform.position - targetPosition).magnitude <= epi) {
				isMoving = false;
				progress = 0f;
				transform.position = targetPosition;
			}
			progress += 0.05f;
		}
	}
	
	public Vec2i getPosition() {
		return position;
	}

	public void setMoveToPosition(Vec2i pos) {
		// Check valid pos
		this.moveToPosition = pos;

		// Move immediately if valid
		this.position = pos;
		this.targetPosition = mapGenerator.GridToWorld (pos.x, pos.y);

		this.isMoving = true;

	}

}
