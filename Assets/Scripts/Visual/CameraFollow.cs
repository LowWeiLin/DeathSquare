using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	
	GameObject target;
	bool disabled = true;

	float cameraDist = 5f;
	Vector3 offsetBase = new Vector3(Mathf.Sqrt (9f/2f),2f,Mathf.Sqrt (9f/2f));
	Vector3 offset;

	void Start() {
		offsetBase.Normalize ();
		offset = offsetBase * cameraDist;
		StartCoroutine(LookForPlayer());
	}
	
	IEnumerator LookForPlayer() {
		GameObject playerObj = null;
		while (disabled) {
			playerObj = GameObject.FindGameObjectWithTag("Player");
			if (playerObj == null) {
				yield return new WaitForSeconds(1f);
			} else {
				break;
			}
		}
		Util.Assert(playerObj != null);
		Init(playerObj);
	}
	
	public void Init(GameObject playerObj) {
		target = playerObj;
		disabled = false;
	}

	void Update() {
		if (target != null) {
			transform.position = target.transform.position + offset;
			transform.LookAt (target.transform);
		} else {
			if (disabled != true) {
				disabled = true;
				StartCoroutine (LookForPlayer ());
			}
		}
	}
}
