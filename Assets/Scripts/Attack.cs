using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {

	public int damage = 1;
	public float cooldown = 1f;
	public float range = 1.5f;

	public float preDelay = 0.1f;
	public float postDelay = 0.1f;

	bool onCooldown = false;

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

	IEnumerator ProcessAttack(GameObject target, Health targetHealth) {

		onCooldown = true;

		Movement movement = GetComponent<Movement>();
		if (movement != null) {
			movement.Pause();
		}

		yield return new WaitForSeconds(preDelay);
		targetHealth.TakeDamage(damage);
		yield return new WaitForSeconds(postDelay);

		yield return new WaitForSeconds(cooldown);
		onCooldown = false;

		if (movement != null) {
			movement.Unpause();
		}
	}
}
