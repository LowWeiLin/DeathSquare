using UnityEngine;
using System.Collections;

public class HumanPlayer : PlayerBase {

	bool attackButton = false;
	bool moveButton = false;
	Dir moveDir;

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
				StartCoroutine(MaybeMove(Dir.Up));
			} else if (Input.GetKey (KeyCode.DownArrow)) {
				StartCoroutine(MaybeMove(Dir.Down));
			} else if (Input.GetKey (KeyCode.LeftArrow)) {
				StartCoroutine(MaybeMove(Dir.Left));
			} else if (Input.GetKey (KeyCode.RightArrow)) {
				StartCoroutine(MaybeMove(Dir.Right));
			}
		}
	}

	IEnumerator MaybeMove(Dir direction) {
		Face(direction);
		yield return new WaitForSeconds(0.09f);
		if (Input.GetKey(direction.ToArrow())) {
			moveButton = true;
			moveDir = direction;
		}
	}
}
