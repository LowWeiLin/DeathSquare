using System.Collections;
using System.Collections.Generic;

public class EntityMap
{
	private Dictionary<Vec2i, List<EntityBase>> entities = new Dictionary<Vec2i, List<EntityBase>>();

	public EntityMap ()
	{
	}

	public bool isOccupied(int x, int y) {
		return isOccupied(new Vec2i(x,y));
	}

	
	public bool isOccupied(Vec2i pos) {
		List<EntityBase> list = null;
		if (entities.TryGetValue(pos, out list)) {
			if (list.Count > 0) {
				foreach(EntityBase e in list) {
					if (e.isCollider) {
						return true;
					}
				}	
			}
		}
		return false;
	}

	public void AddEntity (EntityBase e) {
		List<EntityBase> list = null;
		if (!entities.TryGetValue(e.position, out list)) {
			list = new List<EntityBase>();
			entities.Add(e.position, list);
		}
		list.Add (e);
	}

	public void ChangePosition(EntityBase e, Vec2i newPos) {
		// TODO:

	}
}

