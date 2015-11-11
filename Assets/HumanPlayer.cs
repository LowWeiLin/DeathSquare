using UnityEngine;
using System.Collections;

public class HumanPlayer : PlayerBase {

	// Use this for initialization
	new protected void Start () {
		base.Start ();
	}

	public override void Action () {
		// Something like that.
		setMoveToPosition (new Vec2i(getPosition().x, getPosition().y + 1));
		Debug.Log ("Action");
	}
}
