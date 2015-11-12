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
	public List<PlayerBase> players = new List<PlayerBase>();

	// ===============================
	// 		Entities
	// ===============================
	public EntityMap entityMap = new EntityMap();
	public List<EntityBase> entities = new List<EntityBase>();

	
	void Start () {
		map.Init();
		map.GenerateMap(width, height);
		map.DrawMap (width, height);
	}
	
	void Update () {
		foreach (EntityBase e in new List<EntityBase>(entities)) {
			if (e.CanAct()) {
				e.Action();
			}
		}
	}

	public void RegisterPlayer(PlayerBase player) {
		players.Add(player);
	}

	public void RegisterEntity(EntityBase entity) {
		entities.Add(entity);
		entityMap.AddEntity(entity);
	}

	public bool IsOccupied(Vec2i v) {
		return map.IsOccupied (v) || entityMap.IsOccupied (v);
	}
}
