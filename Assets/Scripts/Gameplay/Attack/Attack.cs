using UnityEngine;
using System.Collections;

public abstract class Attack : MonoBehaviour {

	public int damage = 1;
	public float cooldown = 1f;
	public float range = 1f;

	public float preDelay = 0.1f;
	public float postDelay = 0.1f;

	bool onCooldown = false;

	Maybe<Facing> facing;
	Maybe<Movement> movement;
	protected Maybe<GameObject> model;

	void Start() {
		facing = GetComponent<Facing>();
		movement = GetComponent<Movement>();
		Visuals visuals = GetComponent<Visuals>();
		if (visuals != null) {
			model = visuals.model;
		} else {
			model = null;
		}
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

	// In general attacks take place over time, and the target may have died by the time they complete, hence the Maybes
	public abstract void AttackTarget(Maybe<GameObject> target, Vector3 targetPosition, Maybe<Health> targetHealth);

	IEnumerator ProcessAttack(GameObject target, Health targetHealth) {
		
		// The target may have died by the time this is used, so we get it here
		Vector3 targetPosition = target.transform.position;

		onCooldown = true;
		movement.IfPresent(m => m.Pause());
		facing.IfPresent(f => f.LookAt(target));
			
		yield return new WaitForSeconds(preDelay);
		AttackTarget(target, targetPosition, targetHealth);
		yield return new WaitForSeconds(postDelay);

		yield return new WaitForSeconds(cooldown);

		facing.IfPresent(f => f.StopLooking());
		movement.IfPresent(m => m.Unpause());
		onCooldown = false;
	}
}
