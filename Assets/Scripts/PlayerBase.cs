using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerBase : EntityBase {

	public GameObject projectile;
	
	public new void Init(Vec2i position) {
		base.Init(position);
		willObstruct = true;
		gameController.RegisterPlayer (this);
	}

	// ===============================
	// 		API
	// ===============================
	
	public delegate bool Predicate(EntityBase entity);

	public List<EntityBase> GetEntitiesInRange(int range) {
		return GetEntitiesInRange (range, (e)=>true);
	}
	
	public List<EntityBase> GetPlayersInRange(int range) {
		return GetEntitiesInRange (range, IsPlayer);
	}

	public List<EntityBase> GetEntitiesInRange(int range, Predicate P) {
		List<EntityBase> entities = new List<EntityBase>();
		int y = 0;
		for (int i=-range ; i<=range ; i++) {
			int x = 0;
			for (int j=-range ; j<=range ; j++) {
				Vec2i pos = position + new Vec2i(j,i);
				List<EntityBase> occupants = GetOccupants(pos);
				if (occupants != null) {
					foreach (EntityBase e in occupants) {
						if (P(e)) {
							entities.Add(e);
						}
					}
				}
				x++;
			}
			y++;
		}
		return entities;
	}

	public bool[,] LocalObstructionMap(int radius) {
		if (radius <= 0) {
			return null;
		}

		bool[,] map = new bool[radius*2+1,radius*2+1];
		int y = 0;
		for (int i=-radius ; i<=radius ; i++) {
			int x = 0;
			for (int j=-radius ; j<=radius ; j++) {
				Vec2i pos = position + new Vec2i(j,i);
				map[y,x] = IsObstructed(pos);
			    x++;
			}
			y++;
		}

		return map;
	}

	public EntityBase GetFirstPlayerInDir(Dir dir) {
		return GetFirstEntityInDir (dir, IsPlayer);
	}

	public EntityBase GetFirstEntityInDir(Dir dir) {
		return GetFirstEntityInDir (dir, (e)=>true);
	}

	public EntityBase GetFirstEntityInDir(Dir dir, Predicate p) {
		Vec2i pos = position;
		int searched = 0;
		while (true && searched < 100) { // TODO: set a limit for search
			pos += dir.ToVec();
			List<EntityBase> occupants = GetOccupants(pos);
			if (occupants != null) {
				foreach (EntityBase occupant in occupants) {
					if (p(occupant)) {
						return occupant;
					}
				}
			}
			if (IsOutOfBounds(pos) || false) {
				return null;
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

	public bool IsObstructed(Vec2i position) {
		return gameController.IsObstructed(position);
	}

	public bool IsObstructed(Dir direction) {
		return gameController.IsObstructed(direction.ToVec() + position);
	}

	public bool IsOccupied(Vec2i position) {
		return gameController.IsOccupied(position);
	}

	public List<EntityBase> GetOccupants(Vec2i position) {
		return gameController.GetOccupants(position);
	}

	public bool IsOccupied(Dir direction) {
		return IsOccupied(direction.ToVec() + position);
	}

	public void Attack() {
		foreach (Transform child in transform) {
			if (child.gameObject.GetComponent<Weapon>() != null) {
				child.gameObject.GetComponent<Weapon> ().Attack(this);
			}
		}
	}
}
