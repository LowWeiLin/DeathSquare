using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map : MonoBehaviour {

	// Prefabs
	public GameObject wall;
	public GameObject floor;
	public GameObject[] concreteFloorModels;
	public GameObject[] graveModels;

	// Parent game objects to hold other models
	public GameObject floorTilesParent;
	public GameObject decorsParent;

	// Collection of walls
	private List<GameObject> tiles = new List<GameObject>();
	private float tileWidth = 1f;
	private float tileHeight = 1f;
	
	public int[,] map;
	public int width;
	public int height;
	
	private bool initialized = false;

	private LayerMask mask = -1;

	public void Init() {
		if (initialized) {
			return;
		}
		initialized = true;
		mask = LayerMask.NameToLayer ("Walls");
	}
	
	public int[,] Generate (int width, int height) {
		this.width = width;
		this.height = height;

		DungeonGenerator dungeonGenerator = new DungeonGenerator();
		map = dungeonGenerator.Generate (width, height);
		return map;
	}
	
	public void Draw() {

		// Draw floor
		GameObject floorObject = Instantiate(floor, new Vector3(width/2-0.5f, 0, height/2-0.5f), Quaternion.identity) as GameObject;
		floorObject.transform.localScale = new Vector3(0.1f*width, 1.0f, 0.1f*height);
		floorObject.GetComponent<MeshRenderer> ().material.color = new Color (0.5f, 1f, 1f);

		//floorObject.GetComponent<Renderer> ().enabled = false;

		floorTilesParent = new GameObject ("FloorTilesParent");
		decorsParent = new GameObject ("DecorsParent");

		// Destroy existing tiles
		DestroyTiles ();
		
		// Create walls
		for (int y=0 ; y<height ; y++) {
			for (int x=0 ; x<width ; x++) {
				
				GameObject tile;
				// Set walls
				if (map[y,x] == 0) {
					// Place random floor tiles
					GameObject floorTile = Instantiate(concreteFloorModels[Random.Range(0,concreteFloorModels.Length)],
					                                   GridToWorld(new Vector2(x,y)),
					                                   Quaternion.identity) as GameObject;
					floorTile.transform.localScale = new Vector3(0.015f, 0.015f, 0.015f);
					floorTile.transform.Rotate(90, 0, 0);
					floorTile.transform.parent = floorTilesParent.transform;

					// Place decor randomly
					if (Random.Range(0, 5) == 0) {
						GameObject decor = Instantiate(graveModels[Random.Range(0,graveModels.Length)],
						                                   GridToWorld(new Vector2(x,y)) + new Vector3(Random.Range(-0.25f,0.25f),0,Random.Range(-0.25f,0.25f)),
						                                   Quaternion.identity) as GameObject;
						decor.transform.localScale = new Vector3(0.015f, 0.015f, 0.015f);
						decor.transform.Rotate(-90,0,0);
						decor.transform.parent = decorsParent.transform;
					}

				} else {
					tile = Instantiate(wall, GridToWorld(new Vector2(x,y)) + new Vector3(0, 0.25f, 0), Quaternion.identity) as GameObject;
					tile.GetComponent<MeshRenderer> ().material.color = new Color (0.5f, 0.5f, 0.5f);
					tile.transform.parent = transform;
					tile.layer = mask.value;
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

	public Vec2i WorldToGrid(Vector3 w) {
		return new Vec2i(Mathf.RoundToInt(w.x / tileWidth), Mathf.RoundToInt(w.z / tileHeight));
	}
	
	public Vec2i WorldToGrid(Vector2 w) {
		return new Vec2i(Mathf.RoundToInt(w.x / tileWidth), Mathf.RoundToInt(w.y / tileHeight));
	}
	
	public Vector3 WorldToGrid(Vec2i w) {
		return new Vector3(Mathf.RoundToInt(w.x / tileWidth), 0, Mathf.RoundToInt(w.y / tileHeight));
	}
	
	public Vector3 GridToWorld(Vector2 g) {
		return new Vector3(g.x * tileWidth, 0, g.y * tileHeight);
	}
	
	public Vector3 GridToWorld(Vec2i v) {
		return new Vector3(v.x * tileWidth, 0, v.y * tileHeight);
	}
	
	public Vector3 GridToWorld(int x, int y) {
		return new Vector3(x * tileWidth, 0, y * tileHeight);
	}
	
	public bool OutOfBounds(Vec2i v) {
		return v.x < 0 || v.y < 0 || v.x >= width || v.y >= height;
	}
	
	public bool WithinBounds(Vec2i v) {
		return !OutOfBounds(v);
	}
}