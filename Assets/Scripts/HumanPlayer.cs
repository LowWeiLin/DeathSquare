using UnityEngine;
using System.Collections;

public class HumanPlayer : PlayerBase {

	void Start () {
		Init();
	}

	public void Init() {
		base.Init(new Vec2i(1, 1));
	}

	public override void Action () {

		if (Input.GetKey(KeyCode.Space)) {
			Fire();
		}

		Dir direction = default(Dir);
		
		if (Input.GetKey(KeyCode.UpArrow)) {
			direction = Dir.Up;
		}
		else if (Input.GetKey(KeyCode.DownArrow)) {
			direction = Dir.Down;
		}
		else if (Input.GetKey(KeyCode.LeftArrow)) {
			direction = Dir.Left;
		}
		else if (Input.GetKey(KeyCode.RightArrow)) {
			direction = Dir.Right;
		} else {
			return;
		}

		if (!IsOccupied(direction)) {
			Move(direction);
		}
	}
}
