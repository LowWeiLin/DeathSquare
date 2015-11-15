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
		// Will be null if colliding with a wall
		if (entity != null) {
			Health h = entity.GetComponent<Health> ();
			if (h != null) {
				h.TakeDamage (1);
			}
		}
		// Destroy if obstructed.
		if (entity == null || entity.willObstruct) {
			DestroyEntity ();
		}
	}
}
