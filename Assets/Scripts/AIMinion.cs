using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Movement))]
public class AIMinion : MonoBehaviour {

	Movement movement;
	Attack attack;

	void Start() {
		movement = GetComponent<Movement>();
		attack = GetComponent<Attack>();
	}

	Maybe<GameObject> GetClosestEnemy() {
		List<GameObject> enemies = new List<GameObject>();
		// TODO get list of all units which are not on this minion's team
//		if (enemies.Count == 0) {
//			return Maybe<GameObject>.Empty;
//		}
		// TODO get the closest
		return GameObject.FindGameObjectWithTag("Player");
	}

	bool InAttackRange(GameObject enemy) {
		float attackRange = 10f;
		// TODO check if in range
		return false;
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
