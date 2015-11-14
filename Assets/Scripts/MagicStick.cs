﻿using UnityEngine;
using System.Collections;

public class MagicStick : Weapon {

	public override void Attack(PlayerBase player) {
		GameObject shot = Instantiate(player.projectile) as GameObject;
		shot.GetComponent<Projectile>().Init(player.position, player.facing);
	}
}