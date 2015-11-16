using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	// ===============================
	// 		Map
	// ===============================
	public Map map;

	private int width = 50;
	private int height = 50;

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
		Init ();
	}
	
	private bool initialized = false;
	public void Init() {
		if (initialized)
			return;
		initialized = true;

		map.Init();
		Debug.Log ("Generate map");
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

	public void UnregisterPlayer(PlayerBase player) {
		players.Remove(player);
	}
	
	public void UnregisterEntity(EntityBase entity) {
		entities.Remove(entity);
		entityMap.RemoveEntity(entity);
	}

	public bool IsOccupied(Vec2i v) {
		return map.IsOccupied (v) || entityMap.IsOccupied (v);
	}

	public bool IsObstructed(int x, int y) {
		return IsObstructed (new Vec2i (x, y));
	}

	public bool IsObstructed(Vec2i v) {
		return map.IsOccupied (v) || entityMap.IsObstructed (v);
	}

	public List<EntityBase> GetOccupants(Vec2i v) {
		return entityMap.GetOccupants (v);
	}

	// http://stackoverflow.com/questions/3330181/algorithm-for-finding-nearest-object-on-2d-grid
	public Vec2i FindNearestUnobstructed(Vec2i v, int maxDistance=15) {
		int xs = v.x;
		int ys = v.y;

		if (!IsObstructed(v))
		{
			return v;
		}

		for (int d = 1; d<maxDistance; d++)
		{
			for (int i = 0; i < d + 1; i++)
			{
				int x1 = xs - d + i;
				int y1 = ys - i;
				
				// Check point (x1, y1)
				if (!IsObstructed(x1, y1))
				{
					return new Vec2i(x1, y1);
				}
				
				int x2 = xs + d - i;
				int y2 = ys + i;
				
				// Check point (x2, y2)
				if (!IsObstructed(x2, y2))
				{
					return new Vec2i(x2, y2);
				}
			}
			
			
			for (int i = 1; i < d; i++)
			{
				int x1 = xs - i;
				int y1 = ys + d - i;
				
				// Check point (x1, y1)
				if (!IsObstructed(x1, y1))
				{
					return new Vec2i(x1, y1);
				}
				
				int x2 = xs + d - i;
				int y2 = ys - i;
				
				// Check point (x2, y2)
				if (!IsObstructed(x2, y2))
				{
					return new Vec2i(x2, y2);
				}
			}
		}
		
		throw new UnityException ("No unobstructed position found! Try increasing search range! " + v + " maxDist: " + maxDistance);
		//return v;
	}
}
