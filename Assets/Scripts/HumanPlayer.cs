using UnityEngine;
using System.Collections;

public class HumanPlayer : PlayerBase {

	// Use this for initialization
	new protected void Start () {
		position.x = 1;
		position.y = 1;
		base.Start ();
	}

	public override void Action () {
		// Something like that.
		setMoveToPosition (new Vec2i(getPosition().x, getPosition().y + 1));
	}
}
