using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Movement))]
public class AIMinion : MonoBehaviour {

	Movement movement;

	void Start() {
		movement = GetComponent<Movement>();
	}

	Maybe<GameObject> GetClosestEnemy() {
		List<GameObject> enemies = new List<GameObject>();
		// TODO get list of all units which are not on this minion's team
//		if (enemies.Count == 0) {
//			return Maybe<GameObject>.Empty;
//		}
		// TODO get the closest
		return GameObject.Find("Player");
	}

	bool InAttackRange(GameObject enemy) {
		float attackRange = 10f;
		// TODO check if in range
		return false;
	}

	void AttackEnemy(GameObject enemy) {
		// TODO
	}
		
	void Update () {
		GetClosestEnemy().IfPresent(enemy => {
			if (InAttackRange(enemy)) {
				AttackEnemy(enemy);
			} else {
				movement.MoveTowards(enemy, 3f);
			}
		});
	}
}
