﻿using UnityEngine;
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

		
//		Board board = new Board ();
//		board.CreateBoard (map.map, width, height);
//		List<Vec2i> path = board.FindPathVec (new Vec2i (3, 3), new Vec2i (10, 10));
//		foreach (Vec2i v in path) {
//			Debug.Log (v);
//		}
	}
	
	private bool initialized = false;
	public void Init() {
		if (initialized)
			return;
		initialized = true;
		
		map.Init();
		map.Generate(width, height);
		map.Draw();

	}

	public void CreateUnit(GameObject prefab, Vec2i position) {
		position = FindNearestUnobstructed (position);
		Instantiate(prefab, map.GridToWorld(position), Quaternion.identity);
	}
	
	void Update () {
		foreach (EntityBase e in new List<EntityBase>(entities)) {
			if (true) {
				e.Action();
			}
		}
	}
	
	// ===============================
	// 		Player functions
	// ===============================

	public void RegisterPlayer(PlayerBase player) {
		players.Add(player);
	}
	
	public void UnregisterPlayer(PlayerBase player) {
		players.Remove(player);
	}
	
	// ===============================
	// 		Entity functions
	// ===============================

	public void RegisterEntity(EntityBase entity) {
		entities.Add(entity);
		entityMap.AddEntity(entity);
	}
	
	public void UnregisterEntity(EntityBase entity) {
		entities.Remove(entity);
		entityMap.RemoveEntity(entity);
	}
	
	// ===============================
	// 		Map functions
	// ===============================
	
	public bool IsOccupied(Vec2i v) {
		return map.IsOccupied (v);
	}
	
	public bool IsObstructed(int x, int y) {
		return IsObstructed (new Vec2i (x, y));
	}
	
	public bool IsObstructed(Vec2i v) {
		return map.IsOccupied (v);
	}
	
	public bool IsUnobstructed(int x, int y) {
		return !IsObstructed (new Vec2i (x, y));
	}
	
	public bool IsUnobstructed(Vec2i v) {
		return !IsObstructed (v);
	}
	

	public delegate bool Predicate(int x, int y);
	
	public Vec2i FindNearestUnobstructed(Vec2i v, int maxDistance=15) {
		return FindNearest(v, IsUnobstructed, maxDistance);
	}
	
	// http://stackoverflow.com/questions/3330181/algorithm-for-finding-nearest-object-on-2d-grid
	public Vec2i FindNearest(Vec2i v, Predicate p, int maxDistance=15) {
		int xs = v.x;
		int ys = v.y;
		
		if (p(xs, ys))
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
				if (p(x1, y1))
				{
					return new Vec2i(x1, y1);
				}
				
				int x2 = xs + d - i;
				int y2 = ys + i;
				
				// Check point (x2, y2)
				if (p(x2, y2))
				{
					return new Vec2i(x2, y2);
				}
			}
			
			
			for (int i = 1; i < d; i++)
			{
				int x1 = xs - i;
				int y1 = ys + d - i;
				
				// Check point (x1, y1)
				if (p(x1, y1))
				{
					return new Vec2i(x1, y1);
				}
				
				int x2 = xs + d - i;
				int y2 = ys - i;
				
				// Check point (x2, y2)
				if (p(x2, y2))
				{
					return new Vec2i(x2, y2);
				}
			}
		}
		
		throw new UnityException ("No position found! Try increasing search range! " + v + " maxDist: " + maxDistance);
		//return new Vec2i(-1,-1);
	}
}