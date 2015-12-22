using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Board {
	Cell [,]board;
	AStar pathFinder = new AStar();

	public void CreateBoard(int[,] map, int width, int height) {
		board = new Cell[width,height];
		for (int i = 0; i < height; i++)
		for (int j = 0; j < width; j++) {
			Cell c = new Cell();
			c.coordinates = new Vector2(j, i);
			board[j,i] = c;
			c.walkable = (map[i,j] == 0);
		}
	}
	
	public List<Cell> FindPath(Cell origin, Cell goal) {
		pathFinder.Init ();
		pathFinder.FindPath (origin, goal, board, false);
		return pathFinder.CellsFromPath ();
	}

	public List<Vec2i> FindPathVec(Vec2i origin, Vec2i goal) {
		Cell org = board [origin.x, origin.y];
		Cell dst = board [goal.x, goal.y];
		List<Cell> path = FindPath (org, dst);
		List<Vec2i> pathVec = new List<Vec2i>();
		foreach (Cell cell in path) {
			pathVec.Add(new Vec2i((int)cell.coordinates.x, (int)cell.coordinates.y));
		}
		return pathVec;
	}
}