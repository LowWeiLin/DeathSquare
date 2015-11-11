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
		if(Input.GetKey(KeyCode.UpArrow))
		{	
			setMoveToPosition (new Vec2i(getPosition().x, getPosition().y + 1));
		}
		else if(Input.GetKey(KeyCode.DownArrow))
		{	
			setMoveToPosition (new Vec2i(getPosition().x, getPosition().y - 1));
		}
		else if(Input.GetKey(KeyCode.LeftArrow))
		{	
			setMoveToPosition (new Vec2i(getPosition().x - 1, getPosition().y));
		}
		else if(Input.GetKey(KeyCode.RightArrow))
		{	
			setMoveToPosition (new Vec2i(getPosition().x + 1, getPosition().y));
		}
	}
}
