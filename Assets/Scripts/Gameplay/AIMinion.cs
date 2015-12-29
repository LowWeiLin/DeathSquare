using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Movement))]
public class AIMinion : MonoBehaviour {

	GameController controller;
	Movement movement;
	Attack attack;

	void Start() {
		controller = GameController.Instance;
		movement = GetComponent<Movement>();
		attack = GetComponent<Attack>();
	}

	Maybe<GameObject> GetClosestEnemy() {
		List<GameObject> enemyUnits = controller.GetEnemyUnits (this.gameObject, 5);
		return controller.GetClosest (this.gameObject, enemyUnits);
	}

	void Update () {
		Maybe<GameObject> enemy = GetClosestEnemy ();
		if (enemy.HasValue) {
			if (attack.InRange (enemy.Value)) {
				attack.Hit (enemy.Value);
			} else {
				movement.RouteTowards (enemy.Value);
			}
		} else {
			movement.Wander ();
		}
	}
}
