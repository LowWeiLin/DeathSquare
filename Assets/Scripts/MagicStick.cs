using UnityEngine;
using System.Collections;

public class MagicStick : WeaponBase {

	public GameObject projectile;

	void Start() {
		Init ();
	}
	
	public void Init() {
		base.Init(new Vec2i(0,0));
		base.Init(gameController.FindNearestUnobstructed(new Vec2i(Random.Range(0,20),Random.Range(0,20))));
	}

	public override float cooldown {
		get { return 0.7f; }
		protected set {}
	}

	public override void PerformAttack(PlayerBase attacker) {
		GameObject shot = Instantiate(projectile) as GameObject;
		shot.GetComponent<ProjectileBase>().Init(attacker);
	
	}
}
