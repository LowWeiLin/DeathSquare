using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	// ===============================
	// 		Map
	// ===============================
	public MapGenerator mapGenerator;
	private int[,] globalMap;

	private int width = 16;
	private int height = 16;

	// ===============================
	// 		Players
	// ===============================
	private List<GameObject> playerList = new List<GameObject>();

	// ===============================
	// 		Entities
	// ===============================
	private Dictionary<Vec2i, List<GameObject>> entityList = new Dictionary<Vec2i, List<GameObject>>();

	
	// Use this for initialization
	void Start () {
		mapGenerator.init ();
		globalMap = mapGenerator.GenerateMap(width, height);
		mapGenerator.DrawMap (globalMap, width, height);

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Register a player in the game
	public void registerPlayer(GameObject playerObject) {
		playerList.Add (playerObject);
	}

}
