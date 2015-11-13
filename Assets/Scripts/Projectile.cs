using UnityEngine;
using System.Collections;

public class Projectile : EntityBase {

	public new void Init(Vec2i position) {
		base.Init(position);
	}

	public override void Action () {
		Vec2i offset = new Vec2i(0, 1);
		Vec2i destination = position + offset;

		setMoveToPosition(destination);

		if (IsOutOfBounds(position)) {
			Destroy(gameObject);
		}
	}

	public override void OnCollision(EntityBase entity) {
		DestroyEntity ();
	}
}
