using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIPlayer : PlayerBase {
	
	void Start () {
		Init();
	}
	
	public void Init() {
		base.Init(new Vec2i(0,0));
		base.Init(gameController.FindNearestUnobstructed (new Vec2i(20, 20)));
	}
	
	public override void Action () {
		
		// Pick up all pickables at position.
		List<EntityBase> pickables = GetPickableEntities ();
		for (int i=0 ; i<pickables.Count ; i++) {
			Pickable item = pickables[i].gameObject.GetComponent<Pickable>();
			
			if (item.CanBePicked(this)) {
				item.Pick(this);
				
				// Equip weapon if able to.
				Equipable equip = item.gameObject.GetComponent<Equipable>();
				if (equip && equip.CanEquip("weapon")) {
					equip.Equip("weapon");
				}
				
			}
		}

		// Attack
		foreach (Dir d in Dir.GetValues(typeof(Dir))) {
			if (GetFirstPlayerInDir(d)) {
				Face(d);
				Attack();
			}
		}

		// Move
		for (int i=0 ; i<Dir.GetValues(typeof(Dir)).Length ; i++) {
			if (IsObstructed (facing)) {
				facing = facing.RotateCW();
			} else {
				break;
			}
		}
		Move (facing);

	}

}
