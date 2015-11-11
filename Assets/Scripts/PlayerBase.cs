using UnityEngine;
using System.Collections;

public class PlayerBase : EntityBase {

	// Use this for initialization
	new protected void Start () {
		base.Start ();
		isCollider = true;
		gameController.registerPlayer (this.gameObject);
	}

	public virtual void Action () {
	
	}
}
