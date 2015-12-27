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
	Maybe<GameObject> model;

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

	public void MeleeAttack(Vector3 targetPosition, Maybe<Health> targetHealth) {
		float halfTime = 0.15f;

		model.IfPresent(m => {
			Vector3 basePosition = m.transform.localPosition;

			GoTweenChain chain = new GoTweenChain()
				.append(new GoTween(m.transform, halfTime, new GoTweenConfig()
					.position(targetPosition)
					.setEaseType(GoEaseType.BackIn)))
				.append(new GoTween(m.transform, halfTime, new GoTweenConfig()
					.localPosition(basePosition)
					.setEaseType(GoEaseType.BackOut)));

			// The target may have died by the time the tween completes, hence the Maybe<Health>
			chain.setOnCompleteHandler(c => targetHealth.IfPresent(t => t.TakeDamage(damage)));
			chain.play();
		});			
	}

	IEnumerator ProcessAttack(GameObject target, Health targetHealth) {

		// The target may have died by the time this is used, so we get it here
		Vector3 targetPosition = target.transform.position;

		onCooldown = true;
		movement.IfPresent(m => m.Pause());
		facing.IfPresent(f => f.LookAt(target));
			
		yield return new WaitForSeconds(preDelay);
//		ProjectileAttack(target, targetHealth);
		MeleeAttack(targetPosition, targetHealth);
//		InstantAttack(targetHealth);
		yield return new WaitForSeconds(postDelay);

		yield return new WaitForSeconds(cooldown);

		facing.IfPresent(f => f.StopLooking());
		movement.IfPresent(m => m.Unpause());
		onCooldown = false;
	}
}
