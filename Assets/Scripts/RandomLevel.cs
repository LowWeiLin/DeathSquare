using UnityEngine;
using System.Collections;

public class RandomLevel : MonoBehaviour {

	public GameObject player, minion;

	GameController controller;

	void Start () {
		controller = GameObject.Find("GameController").GetComponent<GameController>();
		controller.Init ();

		controller.CreateUnit(player, new Vec2i(5, 5));
		controller.CreateUnit(minion, new Vec2i(5, 6));
	}
}
