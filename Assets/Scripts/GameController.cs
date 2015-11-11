using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	// ===============================
	// 		Map
	// ===============================
	public Map map;

	private int width = 16;
	private int height = 16;

	// ===============================
	// 		Players
	// ===============================
	public List<PlayerBase> playerBaseList = new List<PlayerBase>();

	// ===============================
	// 		Entities
	// ===============================
	public EntityMap entityMap = new EntityMap();

	
	// Use this for initialization
	void Start () {
		map.init ();
		map.GenerateMap(width, height);
		map.DrawMap (width, height);

	}
	
	// Update is called once per frame
	void Update () {
	
		foreach (PlayerBase player in playerBaseList) {
			// TODO: check if player can make action.
			if (!player.isMoving) {
				player.Action();
			}
		}

	}

	// Register a player in the game
	public void registerPlayer(GameObject playerObject) {
		playerBaseList.Add (playerObject.GetComponent<PlayerBase>());
		entityMap.AddEntity (playerObject.GetComponent<EntityBase>());
	}

	public bool isOccupied(Vec2i v) {
		return map.isOccupied (v) || entityMap.isOccupied (v);
	}
}
