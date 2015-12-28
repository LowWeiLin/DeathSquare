using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Movement))]
public class Projectile : MonoBehaviour {

	public GameObject cubeExplosionPrefab;

	Movement movement;
	Maybe<GameObject> target;
	int damage;

	bool active = false;

	void Start() {
		movement = GetComponent<Movement>();
	}

	public void Fire(int damage, Maybe<GameObject> target) {
		this.target = target;
		this.damage = damage;
		active = true;
	}

	void Update() {
		if (!active) {
			return;
		}
		if (target.IsPresent) {
			movement.MoveTowards(target.Value);
		} else {
			DamageTarget();
		}
	}

	void OnTriggerEnter(Collider other) {
		target.IfPresent(t => {
			if (t == other.gameObject) {
				DamageTarget();
			}
		});
	}

	void DamageTarget() {
		Destroy(gameObject);
		Instantiate(cubeExplosionPrefab, transform.position, Quaternion.identity);
		target.IfPresent(t =>
			Maybe<Health>.Of(t.gameObject.GetComponent<Health>()).IfPresent(
				th => th.TakeDamage (damage)));
	}
}
