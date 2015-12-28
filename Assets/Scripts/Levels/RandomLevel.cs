using UnityEngine;
using System.Collections;

public class RandomLevel : MonoBehaviour {

	public GameObject playerPrefab, minionPrefab;
	GameController controller;

	void Start () {
		controller = GameController.Instance;
		controller.Init ();

		controller.player = controller.CreateUnit(playerPrefab, new Vec2i(5, 5), 1, 100);
		for (int i=0 ; i<50 ; i++)
			controller.CreateUnit(minionPrefab, new Vec2i(Random.Range(1,49), Random.Range(1,49)), 0);
	}

	void Update() {
		EndGameCheck ();
	}

	bool EndGameCheck() {
		if (VictoryCondition()) {
			Debug.Log("Victory!");
			return true;
		}
		if (DefeatCondition()) {
			Debug.Log("Defeat!");
			return true;
		}
		return false;
	}

	bool VictoryCondition() {
		if (controller.player == null) {
			return false;
		}
		// No ememies left
		if (controller.GetEnemyUnits(controller.player).Count == 0) {
			return true;
		}
		return false;
	}
	
	bool DefeatCondition() {
		// Player is dead
		if (controller.player == null) {
			return true;
		}
		return false;
	}
}
