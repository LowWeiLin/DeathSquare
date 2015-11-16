using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DungeonGenerator
{
	public DungeonGenerator ()
	{
	}

	int[,] map;

	public int[,] Generate(int width, int height) {
		map = new int[height, width];
		for (int y=0 ; y<height ; y++) {
			for (int x=0 ; x<width ; x++) {
				map[y,x] = 1;
			}
		}

		List<Rect> rooms = new List<Rect> ();

		int roomMinSize = 7;
		int roomMaxSize = 17;
		int maxTries = 10000;
		int numRooms = 15;

		int tries = 0;
		// Generate rooms
		while (rooms.Count < numRooms && tries < maxTries) {
			tries++;
			Rect room = new Rect(Random.Range(0, width-1), Random.Range(0,height-1),
			                     Random.Range(roomMinSize, roomMaxSize), Random.Range(roomMinSize, roomMaxSize));
			bool overlap = false;
			for (int j=0 ; j<rooms.Count ; j++) {
				// Reject if overlaps another room
				if (room.Overlaps(rooms[j])) {
					overlap = true;
					break;
				}
			}
			if(!overlap) {
				if (!((room.x+room.width >= width) || (room.y+room.height >= height))) {
					rooms.Add(room);
				}
			}
		}

		// List of int pairs
		List<Vec2i> corridors = new List<Vec2i> ();
		// Select corridors
		for (int i=0 ; i<rooms.Count ; i++) {
			/*
			// Random chance to connect rooms
			for (int j=0 ; j<rooms.Count ; j++) {
				if (i != j && Random.Range(0,rooms.Count-1)==0) {
					corridors.Add(new Vec2i(i, j));
				}
			}*/

			// Connect to a random room
			int j = Random.Range(0, rooms.Count);
			if (j == i){
				j++;
				j%=rooms.Count;
			}
			corridors.Add(new Vec2i(i, j));
		}

		// Carve rooms
		for (int i=0 ; i<rooms.Count ; i++) {
			Rect room = rooms[i];
			for (int y=(int)room.y+1 ; y<(int)(room.y+room.height-1) ; y++) {
				for (int x=(int)room.x+1 ; x<(int)(room.x+room.width-1) ; x++) {
					if (y < 0 || y >= height || x<0 || x >= width)
						continue;
					map[y,x] = 0;
				}
			}
		}

		// Carve corridors
		for (int i=0; i<corridors.Count; i++) {
			Rect room1 = rooms[corridors[i].x];
			Rect room2 = rooms[corridors[i].y];

			int x1 = (int)Mathf.Ceil(room1.center.x);
			int y1 = (int)Mathf.Ceil(room1.center.y);
			int x2 = (int)Mathf.Ceil(room2.center.x);
			int y2 = (int)Mathf.Ceil(room2.center.y);

			int x3 = x2;
			int y3 = y1;
			for (int x=(int)Mathf.Min(x1,x3) ; x<=(int)Mathf.Max(x1,x3) ; x++) {
				map[y3,x] = 0;
			}
			
			for (int y=(int)Mathf.Min(y2,y3) ; y<=(int)Mathf.Max(y2,y3) ; y++) {
				map[y,x3] = 0;
			}

		}
		
		return map;
	}


}

