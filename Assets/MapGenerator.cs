using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapGenerator : MonoBehaviour {

	public GameObject wall;
	public GameObject floor;

	private List<GameObject> tiles = new List<GameObject>();
	private float tileWidth;
	private float tileHeight;


	public void init() {
		tileWidth = floor.GetComponent<SpriteRenderer> ().bounds.size.x;
		tileHeight = floor.GetComponent<SpriteRenderer> ().bounds.size.y;
	}
	
	public int[,] GenerateMap (int width, int height) {
		int[,] map;
		map = new int[height, width];

		for (int i=0 ; i<height ; i++) {
			for (int j=0 ; j<width ; j++) {
				//
				if (i==0 || j==0 || i==width-1 || j==height-1) {
					map[j,i] = 1;
				}
			}
		}

		return map;
	}

	public void DrawMap(int[,] map, int width, int height) {


		// Destroy existing tiles
		DestroyTiles ();

		// Create new tiles
		for (int i=0 ; i<height ; i++) {
			for (int j=0 ; j<width ; j++) {

				GameObject tile;
				// Set tiles
				if (map[j,i] == 0) {
					tile = (GameObject)Instantiate(floor, GridToWorld(new Vector2(j,i)), Quaternion.identity);
				} else {
					tile = (GameObject)Instantiate(wall, GridToWorld(new Vector2(j,i)), Quaternion.identity);
				}
				tiles.Add(tile);
			}
		}
	}

	public void DestroyTiles() {
		foreach (GameObject obj in tiles) { 
			Destroy(obj);
		}
		tiles.Clear ();
	}

	public Vector2 WorldToGrid(Vector2 w) {
		return new Vector2(Mathf.Floor(w.x / tileWidth), Mathf.Floor(w.y / tileHeight));
	}
	
	public Vector2 GridToWorld(Vector2 g) {
		return new Vector2(g.x * tileWidth, g.y * tileHeight);
	}
	
	public bool OutOfBounds(Vector2 v, int gridWidth, int gridHeight) {
		Vector2 world = WorldToGrid(v);
		return world.x < 0 || world.y < 0 || world.x >= gridWidth || world.y >= gridHeight;
	}
	
	public bool WithinBounds(Vector2 v, int gridWidth, int gridHeight) {
		return !OutOfBounds(v, gridWidth, gridHeight);
	}

}
