using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIMinion : MonoBehaviour {

	Maybe<GameObject> GetClosestEnemy() {
		List<GameObject> enemies = new List<GameObject>();
		// TODO get list of all units which are not on this minion's team
		if (enemies.Count == 0) {
			return Maybe<GameObject>.Empty;
		}
		// TODO get the closest
		return enemies[0];
	}

	bool InAttackRange(GameObject enemy) {
		float attackRange = 10f;
		// TODO check if in range
		return false;
	}

	void AttackEnemy(GameObject enemy) {
		// TODO
	}

	void MoveTowards(GameObject enemy) {
		// TODO
	}
	
	void Update () {
		GetClosestEnemy().IfPresent(enemy => {
			if (InAttackRange(enemy)) {
				AttackEnemy(enemy);
			} else {
				MoveTowards(enemy);
			}
		});
	}
}
