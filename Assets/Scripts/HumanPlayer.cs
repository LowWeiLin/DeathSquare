using UnityEngine;
using System.Collections;

public class HumanPlayer : PlayerBase {

	// Use this for initialization
	new protected void Start () {
		position = new Vec2i(1, 1);
		base.Start ();
	}

	public override void Action () {

		Vec2i offset = new Vec2i(0, 0);

		if (Input.GetKey(KeyCode.UpArrow)) {
			offset = new Vec2i(0, 1);
		}
		else if (Input.GetKey(KeyCode.DownArrow)) {
			offset = new Vec2i(0, -1);
		}
		else if (Input.GetKey(KeyCode.LeftArrow)) {
			offset = new Vec2i(-1, 0);
		}
		else if (Input.GetKey(KeyCode.RightArrow)) {
			offset = new Vec2i(1, 0);
		}

		Vec2i destination = getPosition() + offset;

		if (!IsOccupied(destination)) {
			setMoveToPosition(destination);
		}
	}
}
