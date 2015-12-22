using UnityEngine;
using System.Collections;

public class RandomLevel : MonoBehaviour {

	public GameObject player, minion;

	GameController controller;

	void Start () {
		controller = GameObject.Find("GameController").GetComponent<GameController>();
		controller.Init ();

		controller.CreateUnit(player, new Vec2i(5, 5));
		for (int i=0 ; i<20 ; i++)
			controller.CreateUnit(minion, new Vec2i(Random.Range(40,49), Random.Range(40,49)));
	}
}
