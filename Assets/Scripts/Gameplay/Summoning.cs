using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Facing))]
public class Summoning : MonoBehaviour {

	public GameObject smoke;
	public GameObject minion;
	Facing facing;
	Maybe<Team> team;

	void Start () {
		facing = GetComponent<Facing>();
		team = GetComponent<Team>();
	}

	IEnumerator SpawnUnit() {
		// This makes it possible for units to spawn inside something and get pushed out of the map
//		float max = 1f;
//		float min = 0.5f;
//		Vector3 position = Random.insideUnitCircle * max;
//		if (Vector3.Distance(position, transform.position) < min) {
//			position = position.normalized * min;
//		}

		Vector3 offset = facing.Direction.normalized;
		float offsetDist = 0.8f;
		offset.Scale(new Vector3(offsetDist, offsetDist, offsetDist));
		Vector3 position = transform.position + offset;

		Instantiate(smoke, position, smoke.transform.rotation);

		yield return new WaitForSeconds(0.2f);

		GameController.Instance.CreateUnit(minion, position, team);
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			StartCoroutine(SpawnUnit());
		}
	}
}
