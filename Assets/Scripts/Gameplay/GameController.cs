using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	static GameController _instance;
	public static GameController Instance {
		get {
			if (_instance == null) {
				_instance = GameObject.Find("GameController").GetComponent<GameController>();
			}
			return _instance;
		}
	}

	// ===============================
	// 		Map
	// ===============================
	public Map map;
	
	private int width = 50;
	private int height = 50;
	
	// ===============================
	// 		Units
	// ===============================
	public List<GameObject> units = new List<GameObject>();
	
	// ===============================
	// 		Entities
	// ===============================
	public List<GameObject> entities = new List<GameObject>();
	
	void Start () {
		Init ();
	}
	
	private bool initialized = false;
	public void Init() {
		if (initialized)
			return;
		initialized = true;
		
		map.Init();
		map.Generate(width, height);
		map.Draw();

	}

	public void SetTeam(GameObject obj, int team) {
		Team teamComponent = obj.GetComponent<Team> ();
		if (teamComponent == null) {
			teamComponent = obj.AddComponent<Team>();
		}
		teamComponent.team = team;
	}

	public void SetHealth(GameObject obj, int health) {
		Health healthComponent = obj.GetComponent<Health> ();
		if (healthComponent == null) {
			healthComponent = obj.AddComponent<Health>();
		}
		healthComponent.maxValue = health;
		healthComponent.value = health;
	}

	public GameObject GetClosest(GameObject obj, List<GameObject> objList) {
		if (objList.Count == 0) {
			return null;
		}
		float minDist = float.MaxValue;
		GameObject closest = objList[0];
		foreach (GameObject o in objList) {
			float dist = Vector3.Distance(o.transform.position, obj.transform.position);
			if (dist < minDist) {
				minDist = dist;
				closest = o;
			}
		}
		return closest;
	}
	
	// ===============================
	// 		Unit functions
	// ===============================

	public void CreateUnit(GameObject prefab, Vec2i position, int team, int hp=10) {
		position = FindNearestUnobstructed (position);
		GameObject unit = (GameObject) Instantiate(prefab,
		                                           map.GridToWorld(position) + new Vector3(Random.Range(0,0.1f), 0, Random.Range(0,0.1f)),
		                                           Quaternion.identity);
		SetTeam (unit, team);
		SetHealth (unit, hp);
		RegisterUnit (unit);
	}

	public void RegisterUnit(GameObject unit) {
		units.Add(unit);
	}
	
	public void UnregisterUnit(GameObject unit) {
		units.Remove(unit);
	}

	public List<GameObject> GetAllUnits() {
		return units;
	}

	public List<GameObject> GetEnemyUnits(GameObject unit, float range=float.MaxValue) {
		Team unitTeamComponent = unit.GetComponent<Team> ();
		List<GameObject> enemyUnits = new List<GameObject> ();
		if (unitTeamComponent == null) {
			unitTeamComponent = new Team();
			unitTeamComponent.transform.parent = unit.transform;
		}

		foreach (GameObject u in units) {
			if (u == unit)
				continue;
			Team teamComponent = u.GetComponent<Team> ();
			if (teamComponent == null || teamComponent.IsEnemyTeam(unitTeamComponent)) {
				if (Vector3.Distance(u.transform.position, unit.transform.position) <= range) {
					enemyUnits.Add(u);
				}
			}
		}

		return enemyUnits;
	}
	
	// ===============================
	// 		Entity functions
	// ===============================

	public void RegisterEntity(GameObject entity) {
		entities.Add(entity);
	}
	
	public void UnregisterEntity(GameObject entity) {
		entities.Remove(entity);
	}
	
	// ===============================
	// 		Map functions
	// ===============================
	
	public bool IsOccupied(Vec2i v) {
		return map.IsOccupied (v);
	}
	
	public bool IsObstructed(int x, int y) {
		return IsObstructed (new Vec2i (x, y));
	}
	
	public bool IsObstructed(Vec2i v) {
		return map.IsOccupied (v);
	}
	
	public bool IsUnobstructed(int x, int y) {
		return !IsObstructed (new Vec2i (x, y));
	}
	
	public bool IsUnobstructed(Vec2i v) {
		return !IsObstructed (v);
	}
	

	public delegate bool Predicate(int x, int y);
	
	public Vec2i FindNearestUnobstructed(Vec2i v, int maxDistance=50) {
		return FindNearest(v, IsUnobstructed, maxDistance);
	}
	
	// http://stackoverflow.com/questions/3330181/algorithm-for-finding-nearest-object-on-2d-grid
	public Vec2i FindNearest(Vec2i v, Predicate p, int maxDistance=15) {
		int xs = v.x;
		int ys = v.y;
		
		if (p(xs, ys))
		{
			return v;
		}
		
		for (int d = 1; d<maxDistance; d++)
		{
			for (int i = 0; i < d + 1; i++)
			{
				int x1 = xs - d + i;
				int y1 = ys - i;
				
				// Check point (x1, y1)
				if (p(x1, y1))
				{
					return new Vec2i(x1, y1);
				}
				
				int x2 = xs + d - i;
				int y2 = ys + i;
				
				// Check point (x2, y2)
				if (p(x2, y2))
				{
					return new Vec2i(x2, y2);
				}
			}
			
			
			for (int i = 1; i < d; i++)
			{
				int x1 = xs - i;
				int y1 = ys + d - i;
				
				// Check point (x1, y1)
				if (p(x1, y1))
				{
					return new Vec2i(x1, y1);
				}
				
				int x2 = xs + d - i;
				int y2 = ys - i;
				
				// Check point (x2, y2)
				if (p(x2, y2))
				{
					return new Vec2i(x2, y2);
				}
			}
		}
		
		throw new UnityException ("No position found! Try increasing search range! " + v + " maxDist: " + maxDistance);
		//return new Vec2i(-1,-1);
	}
}