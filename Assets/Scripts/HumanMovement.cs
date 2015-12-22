using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Movement))]
public class HumanMovement : MonoBehaviour {

	Movement movement;

	void Start () {
		movement = GetComponent<Movement>();
	}

	void Update () {

		if( Input.GetMouseButtonDown(0) )
		{
			Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
			RaycastHit hit;
			
			if( Physics.Raycast( ray, out hit, 100 ) )
			{
				Debug.Log(hit.transform.gameObject.name);
				if (hit.transform.gameObject.name.StartsWith("Floor")) {
					transform.position = hit.point;
				}
			}
		}


		float dx = Input.GetAxisRaw("Horizontal");
		float dy = Input.GetAxisRaw("Vertical");

		float speed;
		if (dx != 0 && dy != 0) {
			speed = 2.0f;
		} else {
			speed = 3.0f;
		}

		movement.Move(dx, dy, speed);
	}
}
