using UnityEngine;
using System.Collections;

public struct Vec2i {
	public readonly int x;
	public readonly int y;
	
	public Vec2i(int x, int y) {
		this.x = x;
		this.y = y;
	}
	
	public override int GetHashCode() {
		return x * 10000 + y;	
	}
	
	public override bool Equals (object v)
	{
		if (! (v is Vec2i)) {
			return false;
		} else {
			return (Vec2i)v == this;
		}
	}
	
	public bool IsAdjacent(Vec2i v) {
		if (Mathf.Abs(v.x - x) <= 1 || Mathf.Abs(v.y - y) <= 1) {
			return true;
		}
		return false;
	}

	public int ManhattanDistance(Vec2i v) {
		return (int) (Mathf.Abs (v.x - x) + Mathf.Abs (v.y - y));
	}

	public float EucledianDistance(Vec2i v) {
		return Mathf.Sqrt ((v.x - x)*(v.x - x) + ((v.y - y)*(v.y - y)));
	}
	
	public static Vec2i operator +(Vec2i left, Vec2i right) {
		return new Vec2i(right.x + left.x, right.y + left.y);
	}
	
	public static Vec2i operator -(Vec2i left, Vec2i right) {
		return new Vec2i(right.x - left.x, right.y - left.y);
	}
	
	public static bool operator ==(Vec2i left, Vec2i right) {
		return left.x == right.x && left.y == right.y;
	}
	
	public static bool operator !=(Vec2i left, Vec2i right) {
		return !(left == right);
	}

	public Vector3 ToVec3 () {
		return new Vector3 (x, 0, y);
	}

	public override string ToString () {
		return string.Format("({0}, {1})", x, y);
	}
}