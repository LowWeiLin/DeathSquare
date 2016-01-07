using UnityEngine;
using System.Collections;

public class RangedAttack : Attack {

	public GameObject projectile;

	public override void AttackTarget(Maybe<GameObject> target, Vector3 targetPosition, Maybe<Health> targetHealth) {
		Vector3 direction = (targetPosition - transform.position).normalized;
		float offset = 0.4f;
		Vector3 projectilePosition = transform.position + direction * offset;

		projectilePosition += projectile.transform.position;

		GameObject p = Instantiate(projectile, projectilePosition, Quaternion.identity) as GameObject;
		p.GetComponent<Projectile>().Fire(damage, target);
	}
}
