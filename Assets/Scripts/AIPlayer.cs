using UnityEngine;
using System.Collections;

public class AIPlayer : PlayerBase {
	
	void Start () {
		Init();
	}
	
	public void Init() {
		base.Init(new Vec2i(0,0));
		base.Init(gameController.FindNearestUnobstructed (new Vec2i(20, 20)));
	}
	
	public override void Action () {

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
