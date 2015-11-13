using UnityEngine;
using System.Collections;

public class Projectile : EntityBase {

	Dir direction;

	public void Init(Vec2i position, Dir direction) {
		base.Init(position);
		this.direction = direction;
	}

	public override void Action () {
		Move(direction);
	}

	public override void OnCollision(EntityBase entity) {
		Health h = entity.GetComponent<Health> ();
		if (h != null) {
			h.TakeDamage (1);
		}
		DestroyEntity ();
	}
}
