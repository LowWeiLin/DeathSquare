using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HumanPlayer : PlayerBase {

	bool attackButton = false;
	bool moveButton = false;
	Dir moveDir;
	bool mayMove = true;

	// For testing drop
	bool pickedSomething = false;

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
	
		// Pick up all pickables at position.
		List<EntityBase> pickables = GetPickableEntities ();
		for (int i=0 ; i<pickables.Count ; i++) {
			Pickable item = pickables[i].gameObject.GetComponent<Pickable>();
			Debug.Log ("See item");
			if (!pickedSomething && item.CanBePicked(this)) {
				Debug.Log ("Picked item");
				item.Pick(this);
				pickedSomething = true;
			}
		}


		// Test dropping item
		List<Pickable> items = inventory.GetItems ();
		for (int i=0 ; i<items.Count ; i++) {
			if (items[i].CanBeDropped(this)) {
				items[i].Drop(this);
				Debug.Log ("Dropped item");
			}
		}


		attackButton = false;
		moveButton = false;
	}

	public void Update() {
		if (Input.GetKey(KeyCode.Space)) {
			attackButton = true;
		}

		if (moveProgress >= 0.5f && mayMove) {
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
		mayMove = false;
		Face(direction);
		yield return new WaitForSeconds(0.09f);
		if (Input.GetKey(direction.ToArrow())) {
			moveButton = true;
			moveDir = direction;
		}
		mayMove = true;
	}
}
