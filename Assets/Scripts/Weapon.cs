using UnityEngine;
using System.Collections;

public abstract class Weapon : MonoBehaviour {

	public float cooldown;

	bool enabled = true;

	public void Attack(PlayerBase player) {
		if (enabled) {
			PerformAttack(player);
			StartCoroutine(PutOnCooldown ());
		}
	}

	IEnumerator PutOnCooldown() {
		enabled = false;
		yield return new WaitForSeconds(cooldown);
		enabled = true;
	}

	public abstract void PerformAttack(PlayerBase player);
}
