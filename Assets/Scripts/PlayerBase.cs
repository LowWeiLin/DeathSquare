using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerBase : EntityBase {

	public GameObject projectile;
	
	public new void Init(Vec2i position) {
		base.Init(position);
		isCollider = true;
		gameController.RegisterPlayer (this);
	}

	// ===============================
	// 		API
	// ===============================

	public List<EntityBase> GetEntitiesInRange(int range) {
		return GetEntitiesInRange (range, (e)=>true);
	}
	
	public List<EntityBase> GetPlayersInRange(int range) {
		return GetEntitiesInRange (range, IsPlayer);
	}

	public delegate bool Predicate(EntityBase entity);
	public List<EntityBase> GetEntitiesInRange(int range, Predicate P) {
		List<EntityBase> entities = new List<EntityBase>();
		int y = 0;
		for (int i=-range ; i<=range ; i++) {
			int x = 0;
			for (int j=-range ; j<=range ; j++) {
				Vec2i pos = position + new Vec2i(j,i);
				EntityBase e = GetOccupant(pos);
				if (e != null && P(e)) {
					entities.Add(e);
				}
				x++;
			}
			y++;
		}
		return entities;
	}

	public bool[,] LocalOccupancyMap(int radius) {
		if (radius <= 0) {
			return null;
		}

		bool[,] map = new bool[radius*2+1,radius*2+1];
		int y = 0;
		for (int i=-radius ; i<=radius ; i++) {
			int x = 0;
			for (int j=-radius ; j<=radius ; j++) {
				Vec2i pos = position + new Vec2i(j,i);
				map[y,x] = IsOccupied(pos);
			    x++;
			}
			y++;
		}

		return map;
	}

	public EntityBase GetFirstEntityInDir(Dir dir) {
		Vec2i pos = position;
		int searched = 0;
		while (true && searched < 100) { // TODO: set a limit for search
			pos += dir.ToVec();
			if (IsOccupied(pos)) {
				return GetOccupant(pos);
			}
		}
		return null;
	}

	public bool IsPlayer(EntityBase entity) {
		if (entity == null) {
			return false;
		} else if (entity is PlayerBase) {
			return true;
		} else {
			return false;
		}
	}

	public bool IsOccupied(Vec2i position) {
		return gameController.IsOccupied(position);
	}

	public EntityBase GetOccupant(Vec2i position) {
		return gameController.GetOccupant(position);
	}

	public bool IsOccupied(Dir direction) {
		return IsOccupied(direction.ToVec() + position);
	}

	// TODO: is this right?
	public void Fire() {
		GameObject shot = Instantiate(projectile) as GameObject;
		shot.GetComponent<Projectile>().Init(position, facing);
	}
}
