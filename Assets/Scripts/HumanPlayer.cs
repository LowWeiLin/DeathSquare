using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HumanPlayer : PlayerBase {
	
	bool attackButton = false;
	bool moveButton = false;
	Dir moveDir;
	bool mayMove = true;
	
	// For testing drop
	bool pickedSomething = false;
	
	void Start () {
		Init();
	}
	
	public void Init() {
		base.Init(new Vec2i(0,0));
		base.Init(gameController.FindNearestUnobstructed (new Vec2i(1, 1)));
	}
	
	public override void Action () {

	}
	
	public void Update() {
	}
}