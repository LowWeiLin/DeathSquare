using UnityEngine;
using System.Collections;

public class RangedAttack : Attack {

	public GameObject projectile;

	public override void AttackTarget(Maybe<GameObject> target, Vector3 targetPosition, Maybe<Health> targetHealth) {
		Vector3 direction = (targetPosition - transform.position).normalized;
		float offset = 0.01f;
		Vector3 projectilePosition = transform.position + direction * offset;

		// HACK adjust height
		projectilePosition += new Vector3(0f, 0.2f, 0f);

		GameObject p = Instantiate(projectile, projectilePosition, Quaternion.identity) as GameObject;
		p.GetComponent<Projectile>().Fire(damage, target);
	}
}
