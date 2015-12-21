using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EntityBase : MonoBehaviour {

	protected float movementRate = 4f;
	
	public Map map;
	public GameController gameController;
	
	public void Init(Vec2i position) {
		gameController = GameObject.Find ("GameController").GetComponent<GameController> ();
		gameController.Init ();
		
		map = GameObject.Find ("Map").GetComponent<Map> ();
		map.Init ();
		
		SetPosition (position);

		gameController.RegisterEntity (this);
	}
	
	public void SetPosition(Vec2i position) {
		transform.position = map.GridToWorld (position);
	}
	
	public void DestroyEntity() {
		gameController.UnregisterEntity (this);
		Destroy (this.gameObject);
	}

	public virtual void Action() {
		
	}
	
	public virtual void OnCollision(EntityBase entity) {
		
	}

	
	// ===============================
	// 		Entity API
	// ===============================
	
	public bool IsOutOfBounds(Vec2i position) {
		return map.OutOfBounds(position);
	}
	
	public Team GetTeam() {
		Team team = GetComponent<Team> ();
		if (team) {
			return team;
		} else {
			team = gameObject.AddComponent<Team>();
			return team;
		} 
	}
	
}