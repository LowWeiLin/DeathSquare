using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour {

	public GameObject wall;
	public GameObject floor;

	public int width = 1;
	public int height = 4;

	public int[,] map;

	private bool mapChanged = true;
	private float tileSize = 0;

	// Use this for initialization
	void Start () {
		map = new int[height, width];
		tileSize = floor.GetComponent<SpriteRenderer> ().bounds.size.x;
	}
	
	// Update is called once per frame
	void Update () {

		if (mapChanged) {
			mapChanged = false;

			for (int i=0 ; i<height ; i++) {
				for (int j=0 ; j<width ; j++) {
					Instantiate(floor, (new Vector3(j, i, 10))*tileSize, Quaternion.identity);
				}
			}


		}

	}
}
