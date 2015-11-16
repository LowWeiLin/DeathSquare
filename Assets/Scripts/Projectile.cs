using UnityEngine;
using System.Collections;

public class Projectile : EntityBase {

	public Dir direction;
	public PlayerBase attacker;
	public Team team;

	public void Init(PlayerBase attacker) {
		base.Init(attacker.position);
		this.direction = attacker.facing;
		this.attacker = attacker;
		if (this.attacker)
			team = this.attacker.GetTeam ();
		this.movementRate = 6f;
	}

	public void Init(Vec2i position, Dir direction) {
		base.Init(position);
		this.direction = direction;
	}

	public override void Action () {
		Move(direction);
	}

	public override void OnCollision(EntityBase entity) {
		// Do not collide with attacker
		if (entity && entity == attacker) {
			return;
		}

		// Will be null if colliding with a wall
		if (entity != null && !this.GetTeam().IsSameTeam(entity.GetTeam())) {
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
