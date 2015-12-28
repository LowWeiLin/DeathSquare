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
		// TODO get list of all units which are not on this minion's team
		// TODO get the closest
		List<GameObject> enemyUnits = controller.GetEnemyUnits (this.gameObject);
		return controller.GetClosest (this.gameObject, enemyUnits);
	}

	void Update () {
		GetClosestEnemy().IfPresent(enemy => {
			if (attack.InRange(enemy)) {
				attack.Hit(enemy);
			} else {
				//movement.MoveTowards(enemy, 3f);
				movement.RouteTowards(enemy);
			}
		});
	}
}
