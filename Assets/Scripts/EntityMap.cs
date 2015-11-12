using System.Collections;
using System.Collections.Generic;

public class EntityMap {

	private Dictionary<Vec2i, List<EntityBase>> entities = new Dictionary<Vec2i, List<EntityBase>>();

	public bool IsOccupied(int x, int y) {
		return IsOccupied(new Vec2i(x,y));
	}

	public EntityBase GetOccupant(Vec2i pos) {
		List<EntityBase> list = null;
		if (entities.TryGetValue(pos, out list)) {
			if (list.Count > 0) {
				foreach(EntityBase e in list) {
					if (e.isCollider) {
						return e;
					}
				}	
			}
		}
		return null;
	}
	
	public bool IsOccupied(Vec2i pos) {
		return (GetOccupant(pos) != null);
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

