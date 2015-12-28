using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Facing))]
public class Summoning : MonoBehaviour {

	public GameObject smoke;
	public GameObject minion;
	Facing facing;

	void Start () {
		facing = GetComponent<Facing>();
	}

	void SpawnUnit() {
		// This makes it possible for units to spawn inside something and get pushed out of the map
//		float max = 1f;
//		float min = 0.5f;
//		Vector3 position = Random.insideUnitCircle * max;
//		if (Vector3.Distance(position, transform.position) < min) {
//			position = position.normalized * min;
//		}

		Vector3 offset = facing.Direction.normalized;
		offset.Scale(new Vector3(0.5f, 0.5f, 0.5f));
		Vector3 position = transform.position + offset;

		Instantiate(minion, position, minion.transform.rotation);
		Instantiate(smoke, position, smoke.transform.rotation);
	}
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			SpawnUnit();
		}
	}
}
