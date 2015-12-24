using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {

	public GameObject projectile;

	public int damage = 1;
	public float cooldown = 1f;
	public float range = 1f;

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

		Maybe<Health> targetHealth = target.GetComponent<Health>();
		if (!targetHealth.HasValue) {
			return;
		}

		StartCoroutine(ProcessAttack(target, targetHealth.Value));
	}

	public void ProjectileAttack(GameObject target, Health targetHealth) {

		Vector3 direction = (target.transform.position - transform.position).normalized;
		float offset = 0.01f;
		Vector3 projectilePosition = transform.position + direction * offset;

		// HACK adjust height
		projectilePosition += new Vector3(0f, 0.2f, 0f);

		GameObject p = Instantiate(projectile, projectilePosition, Quaternion.identity) as GameObject;
		p.GetComponent<Projectile>().Fire(damage, target);
	}
		
	public void InstantAttack(Health targetHealth) {
		targetHealth.TakeDamage(damage);
	}

	public void MeleeAttack(GameObject target, Health targetHealth) {
		Vector3 originalPosition = GetComponent<Visuals>().model.transform.position;
		float halfTime = 0.15f;

		Go.to(GetComponent<Visuals>().model.transform, halfTime, new GoTweenConfig()
			.position(target.transform.position)
			.setEaseType(GoEaseType.BackIn)
			.onComplete(t =>
				Go.to(GetComponent<Visuals>().model.transform, halfTime, new GoTweenConfig()
					.position(originalPosition)
					.setEaseType(GoEaseType.BackOut)
					.onComplete(t2 => targetHealth.TakeDamage(damage)))
			));
	}

	IEnumerator ProcessAttack(GameObject target, Health targetHealth) {

		onCooldown = true;
		movement.IfPresent(m => m.Pause());
		facing.IfPresent(f => f.LookAt(target));
			
		yield return new WaitForSeconds(preDelay);
//		ProjectileAttack(target, targetHealth);
		MeleeAttack(target, targetHealth);
//		InstantAttack(targetHealth);
		yield return new WaitForSeconds(postDelay);

		yield return new WaitForSeconds(cooldown);

		facing.IfPresent(f => f.StopLooking());
		movement.IfPresent(m => m.Unpause());
		onCooldown = false;
	}
}
