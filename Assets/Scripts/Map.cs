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
	private int width;
	private int height;
	
	private bool initialized = false;
	public void Init() {
		if (initialized)
			return;
		initialized = true;
		
		tileWidth = 1;
		tileHeight = 1;
	}
	
	public int[,] GenerateMap (int width, int height) {
		
		DungeonGenerator dungeonGenerator = new DungeonGenerator ();
		map = dungeonGenerator.Generate (width, height);
		
		return map;
	}
	
	public void DrawMap(int width, int height) {
		this.width = width;
		this.height = height;

		// Draw floor
		GameObject floorObject = Instantiate(floor, new Vector3(width/2-0.5f, 0, height/2-0.5f), Quaternion.identity) as GameObject;
		floorObject.transform.localScale =  new Vector3(0.1f*width, 1.0f, 0.1f*height);
		floorObject.GetComponent<MeshRenderer> ().material.color = new Color (0.5f, 1f, 1f);

		// Destroy existing tiles
		DestroyTiles ();
		
		// Create walls
		for (int y=0 ; y<height ; y++) {
			for (int x=0 ; x<width ; x++) {
				
				GameObject tile;
				// Set walls
				if (map[y,x] == 0) {
				} else {
					tile = Instantiate(wall, GridToWorld(new Vector2(x,y)) + new Vector3(0, 0.5f, 0), Quaternion.identity) as GameObject;
					tile.GetComponent<MeshRenderer> ().material.color = new Color (0.5f, 0.5f, 0.5f);
					tile.transform.parent = transform;
					tiles.Add(tile);
				}
			}
		}
	}
	
	public bool IsOccupied(Vec2i v) {
		// TODO: check if visible.
		if (OutOfBounds(v) || map[v.y, v.x] != 0) {
			return true;
		}
		return false;
	}
	
	public void DestroyTiles() {
		foreach (GameObject obj in tiles) { 
			Destroy(obj);
		}
		tiles.Clear ();
	}
	
	public Vec2i WorldToGrid(Vector2 w) {
		return new Vec2i(Mathf.FloorToInt(w.x / tileWidth), Mathf.FloorToInt(w.y / tileHeight));
	}
	
	public Vector3 WorldToGrid(Vec2i w) {
		return new Vector3(Mathf.Floor(w.x / tileWidth), 0, Mathf.Floor(w.y / tileHeight));
	}
	
	public Vector3 GridToWorld(Vector2 g) {
		return new Vector3(g.x * tileWidth, 0, g.y * tileHeight);
	}
	
	public Vector3 GridToWorld(Vec2i v) {
		return new Vector3(v.x * tileWidth, 0, v.y * tileHeight);
	}
	
	public Vector2 GridToWorld(int x, int y) {
		return new Vector3(x * tileWidth, 0, y * tileHeight);
	}
	
	public bool OutOfBounds(Vec2i v) {
		return v.x < 0 || v.y < 0 || v.x >= width || v.y >= height;
	}
	
	public bool WithinBounds(Vec2i v) {
		return !OutOfBounds(v);
	}
	
}