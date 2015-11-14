using UnityEngine;
using System.Collections;

public class Pickup : EntityBase {

	void Start () {
		Init();
	}

	public void Init() {
		if (transform.parent == null) {
			base.Init(new Vec2i(3, 1));
		} else {
			base.Init(new Vec2i(0, 0));
		}
	}

	public override void OnCollision(EntityBase entity) {
		if (entity is PlayerBase) {
			gameController.entityMap.RemoveEntity(this);
			transform.parent = entity.gameObject.transform;
			transform.localPosition = Vector3.zero;
		}
	}
}
