using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {

	public GameObject projectile;

	public int damage = 1;
	public float cooldown = 1f;
	public float range = 1.5f;

	public float preDelay = 0.1f;
	public float postDelay = 0.1f;

	bool onCooldown = false;

	Maybe<Facing> facing;
	Maybe<Movement> movement;

	void Start() {
		facing = GetComponent<Facing>();
		movement = GetComponent<Movement>();
	}

	public bool InRange(GameObject target) {
		return Vector3.Distance(target.transform.position, transform.position) < range;
	}

	public void Hit(GameObject target) {
		if (onCooldown || !InRange(target)) {
			return;
		}

		Health targetHealth = target.GetComponent<Health>();
		if (targetHealth == null) {
			return;
		}

		StartCoroutine(ProcessAttack(target, targetHealth));
	}

	public void ProjectileAttack(GameObject target, Health targetHealth) {

		Vector3 direction = (target.transform.position - transform.position).normalized;
		float offset = 1f;
		Vector3 projectilePosition = transform.position + direction * offset;

		// HACK adjust height
		projectilePosition += new Vector3(0f, 1f, 0f);

		GameObject p = Instantiate(projectile, projectilePosition, Quaternion.identity) as GameObject;
		p.GetComponent<Projectile>().Fire(damage, target);
	}
		
	public void InstantAttack(Health targetHealth) {
		targetHealth.TakeDamage(damage);
	}

	IEnumerator ProcessAttack(GameObject target, Health targetHealth) {

		onCooldown = true;
		movement.IfPresent(m => m.Pause());
		facing.IfPresent(f => f.LookAt(target));
			
		yield return new WaitForSeconds(preDelay);
		ProjectileAttack(target, targetHealth);
//		InstantAttack(targetHealth);
		yield return new WaitForSeconds(postDelay);

		yield return new WaitForSeconds(cooldown);

		facing.IfPresent(f => f.StopLooking());
		movement.IfPresent(m => m.Unpause());
		onCooldown = false;
	}
}
