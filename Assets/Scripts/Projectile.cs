using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Movement))]
public class Projectile : MonoBehaviour {

	Movement movement;
	GameObject target;
	int damage;

	bool active = false;

	void Start() {
		movement = GetComponent<Movement>();
	}

	public void Fire(int damage, GameObject target) {
		this.target = target;
		this.damage = damage;
		active = true;
	}

	void Update() {
		if (!active) {
			return;
		}
		movement.MoveTowards(target, 8f);
	}

	void OnTriggerEnter(Collider other) {
		Destroy(gameObject);
		Maybe<Health> targetHealth = other.gameObject.GetComponent<Health>();
		targetHealth.IfPresent(t => t.TakeDamage(damage));
	}
}
