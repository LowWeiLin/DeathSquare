using UnityEngine;
using System.Collections;

public class HumanPlayer : PlayerBase {

	private bool attackButton = false;
	private bool moveButton = false;
	private Dir moveDir;

	void Start () {
		Init();
	}

	public void Init() {
		base.Init(new Vec2i(0,0));
		base.Init(gameController.FindNearestUnobstructed (new Vec2i(1, 1)));
	}

	public override void Action () {
		
		if (moveButton) {
			Move(moveDir);
		}

		if (attackButton) {
			Attack ();
		}

		attackButton = false;
		moveButton = false;
	}

	public void Update() {
		if (Input.GetKey(KeyCode.Space)) {
			attackButton = true;
		}

		if (moveProgress >= 0.5f) {
			if (Input.GetKey (KeyCode.UpArrow)) {
				moveButton = true;
				moveDir = Dir.Up;
			} else if (Input.GetKey (KeyCode.DownArrow)) {
				moveButton = true;
				moveDir = Dir.Down;
			} else if (Input.GetKey (KeyCode.LeftArrow)) {
				moveButton = true;
				moveDir = Dir.Left;
			} else if (Input.GetKey (KeyCode.RightArrow)) {
				moveButton = true;
				moveDir = Dir.Right;
			}
		}

	}
}
