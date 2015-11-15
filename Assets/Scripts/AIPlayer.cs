using UnityEngine;
using System.Collections;

public class AIPlayer : PlayerBase {
	
	void Start () {
		Init();
	}
	
	public void Init() {
		base.Init(new Vec2i(10, 10));
	}
	
	public override void Action () {

		foreach (Dir d in Dir.GetValues(typeof(Dir))) {
			if (GetFirstPlayerInDir(d)) {
				Face(d);
				Attack();
				return;
			}
		}

	}

}
