using UnityEngine;
using System.Collections;

public enum Dir {Up, Right, Down, Left}

public static class Extensions
{
	public static Vec2i ToVec(this Dir dir)
	{
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
}
