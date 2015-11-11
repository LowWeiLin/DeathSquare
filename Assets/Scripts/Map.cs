using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map : MonoBehaviour {

	public GameObject wall;
	public GameObject floor;

	private List<GameObject> tiles = new List<GameObject>();
	private float tileWidth;
	private float tileHeight;

	public int[,] map;
	public int width;
	public int height;

	public void init() {
		tileWidth = floor.GetComponent<SpriteRenderer> ().bounds.size.x;
		tileHeight = floor.GetComponent<SpriteRenderer> ().bounds.size.y;
	}
	
	public int[,] GenerateMap (int width, int height) {
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

	public void DrawMap(int width, int height) {
		this.width = width;
		this.height = height;

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
	
	public Vector2 GridToWorld(Vec2i v) {
		return new Vector2(v.x * tileWidth, v.y * tileHeight);
	}
	
	public Vector2 GridToWorld(int x, int y) {
		return new Vector2(x * tileWidth, y * tileHeight);
	}
	
	public bool OutOfBounds(Vector2 v) {
		Vector2 world = WorldToGrid(v);
		return world.x < 0 || world.y < 0 || world.x >= width || world.y >= height;
	}
	
	public bool WithinBounds(Vector2 v) {
		return !OutOfBounds(v);
	}

}
