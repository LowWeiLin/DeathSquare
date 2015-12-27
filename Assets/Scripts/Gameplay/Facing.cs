using UnityEngine;
using System.Collections;

public class Facing : MonoBehaviour {

	Maybe<GameObject> lookingAtTarget;
	Maybe<GameObject> model;

	void Start () {
		Visuals visuals = GetComponent<Visuals>();
		if (visuals != null) {
			model = visuals.model;
		}
	}

	public void Face(Vector3 direction) {
		if (direction == Vector3.zero) {
			return;
		}
		Quaternion newRotation = Quaternion.LookRotation(-direction);

		// HACK compensate for initial rotation of model
		newRotation *= Quaternion.Euler(270, 0, 0);

		model.IfPresent(m =>
			m.transform.rotation = Quaternion.Slerp(m.transform.rotation, newRotation, Time.deltaTime * 8));
	}

	public void LookAt(GameObject target) {
		lookingAtTarget = target;
	}

	public void StopLooking() {
		lookingAtTarget = null;
	}

	void Update () {
		lookingAtTarget.IfPresent(t => Face(t.transform.position - transform.position));
	}
}
