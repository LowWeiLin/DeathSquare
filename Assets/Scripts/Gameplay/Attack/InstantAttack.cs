using UnityEngine;
using System.Collections;

public class InstantAttack : Attack {

	public override void AttackTarget(Maybe<GameObject> target, Vector3 targetPosition, Maybe<Health> targetHealth) {
		targetHealth.IfPresent(th => th.TakeDamage(damage));
	}
}
