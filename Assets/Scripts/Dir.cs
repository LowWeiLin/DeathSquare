using UnityEngine;
using System.Collections;

public enum Dir { Up, Right, Down, Left }

public static class DirExtensions {
	public static Vec2i ToVec(this Dir dir) {
		switch (dir) {
		case Dir.Up:
			return new Vec2i(0,1);
		case Dir.Right:
			return new Vec2i(1,0);
		case Dir.Down:
			return new Vec2i(0,-1);
		case Dir.Left:
			return new Vec2i(-1,0);
		default:
			return new Vec2i(0,0);
		}
	}

	public static Dir ToDir(this Vec2i vec) {
		if (vec.x == 0) {
			if (vec.y == 0) {
				// Default to up
				return Dir.Up;
			} else {
				return vec.y < 0 ? Dir.Up : Dir.Down;
			}
		} else {
			if (vec.y == 0) {
				return vec.x < 0 ? Dir.Right : Dir.Left;
			} else {
				// Take the larger component
				return vec.x > vec.y ? ToDir(new Vec2i(vec.x, 0)) : ToDir(new Vec2i(0, vec.y));
			}
		}
	}

	public static Dir RotateCW(this Dir dir) {
		return (Dir)(((int)dir + 1)%4);
	}

	public static Dir RotateCCW(this Dir dir) {
		return (Dir)(((int)dir + 5)%4);
	}

	public static Dir Rotate180(this Dir dir) {
		return (Dir)(((int)dir + 2)%4);
	}
}
