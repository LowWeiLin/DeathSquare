using UnityEngine;
using System.Collections;

public class MagicStick : Weapon {

	public override float cooldown {
		get { return 0.7f; }
		protected set {}
	}

	public override void PerformAttack(PlayerBase attacker) {
		GameObject shot = Instantiate(attacker.projectile) as GameObject;
		shot.GetComponent<Projectile>().Init(attacker);
	
	}
}
