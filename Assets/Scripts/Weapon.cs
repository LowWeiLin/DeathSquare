﻿using UnityEngine;
using System.Collections;

public abstract class Weapon : MonoBehaviour {

	public float cooldown;

	bool isUsable = true;

	public void Attack(PlayerBase player) {
		if (isUsable) {
			PerformAttack(player);
			StartCoroutine(PutOnCooldown ());
		}
	}

	IEnumerator PutOnCooldown() {
		isUsable = false;
		yield return new WaitForSeconds(cooldown);
		isUsable = true;
	}

	public abstract void PerformAttack(PlayerBase player);
}
