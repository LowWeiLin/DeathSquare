using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Movement))]
public class HumanMovement : MonoBehaviour {

	Movement movement;

	Vector3 routeDestination = Vector3.down;
	float routePrecision = 0.01f;

	void Start () {
		movement = GetComponent<Movement>();
	}

	void Update () {

		// Click on floor to route towards point.
		if( Input.GetMouseButtonDown(0) )
		{
			Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
			RaycastHit hit;
			
			if( Physics.Raycast( ray, out hit, 100 ) )
			{
				Debug.Log(hit.transform.gameObject.name);
				if (hit.transform.gameObject.name.StartsWith("Floor")) {
					// Instant teleport
					//transform.position = hit.point;
					routeDestination = hit.point;
					Debug.Log(hit.point);
				}
			}
		}
		
		if (routeDestination != Vector3.down) {
			if (Vector3.Distance(transform.position, routeDestination) < routePrecision) {
				routeDestination = Vector3.down;
			}
			movement.RouteTowards (routeDestination, routePrecision);
		}

		/*

		float dx = Input.GetAxisRaw("Horizontal");
		float dy = Input.GetAxisRaw("Vertical");

		float speed;
		if (dx != 0 && dy != 0) {
			speed = 2.0f;
		} else {
			speed = 3.0f;
		}

		movement.Move(dx, dy, speed);
		*/
	}
}
