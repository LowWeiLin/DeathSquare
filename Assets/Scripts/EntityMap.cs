using System.Collections;
using System.Collections.Generic;

public class EntityMap {
	
	private List<EntityBase> entities = new List<EntityBase>();
	
	public void AddEntity (EntityBase e) {
		entities.Add (e);
	}
	
	public void RemoveEntity(EntityBase entity) {
		entities.Remove(entity);
	}
}