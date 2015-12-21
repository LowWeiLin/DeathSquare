﻿using System.Collections;
using System.Collections.Generic;

public class EntityMap {
	
	private Dictionary<Vec2i, List<EntityBase>> entities = new Dictionary<Vec2i, List<EntityBase>>();
	
	public List<EntityBase> GetOccupants(Vec2i pos) {
		List<EntityBase> list = null;
		if (entities.TryGetValue(pos, out list)) {
			if (list.Count > 0) {
				return list;
			}
		}
		return null;
	}
	
	public EntityBase GetObstruction(Vec2i pos) {
		List<EntityBase> list = null;
		if (entities.TryGetValue(pos, out list)) {
			if (list.Count > 0) {
				foreach(EntityBase e in list) {
					if (e.willObstruct) {
						return e;
					}
				}	
			}
		}
		return null;
	}
	
	public bool IsOccupied(int x, int y) {
		return IsOccupied(new Vec2i(x, y));
	}
	
	public bool IsOccupied(Vec2i pos) {
		return GetOccupants(pos) != null;
	}
	
	
	public bool IsObstructed(int x, int y) {
		return IsObstructed(new Vec2i(x, y));
	}
	
	public bool IsObstructed(Vec2i pos) {
		return GetObstruction(pos) != null;
	}
	
	public void AddEntity (EntityBase e) {
		List<EntityBase> list = null;
		if (!entities.TryGetValue(e.position, out list)) {
			list = new List<EntityBase>();
			entities.Add(e.position, list);
		}
		list.Add (e);
	}
	
	public void RemoveEntity(EntityBase entity) {
		List<EntityBase> list = null;
		if (entities.TryGetValue(entity.position, out list)) {
			list.Remove(entity);
		}
	}
	
	public void ChangePosition(EntityBase e, Vec2i newPos) {
		RemoveEntity (e);
		e.position = newPos;
		AddEntity (e);
	}
}