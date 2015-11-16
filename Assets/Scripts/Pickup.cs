using UnityEngine;
using System.Collections;

public class Pickup : EntityBase {

	void Start () {
		Init();
	}

	public void Init() {
		base.Init(new Vec2i(0,0));
		base.Init(gameController.FindNearestUnobstructed (new Vec2i(Random.Range(0, 20), Random.Range(0,20))));
	}

	public override void OnCollision(EntityBase entity) {
		if (entity is PlayerBase) {
			gameController.entityMap.RemoveEntity(this);
			transform.parent = entity.gameObject.transform;
			transform.localPosition = Vector3.zero;
		}
	}
}
