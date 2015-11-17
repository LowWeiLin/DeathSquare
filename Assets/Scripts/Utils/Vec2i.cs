﻿using UnityEngine;
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
	
	public bool IsAdjacent(Vec2i v) {
		if (Mathf.Abs(v.x - x) <= 1 || Mathf.Abs(v.y - y) <= 1) {
			return true;
		}
		return false;
	}
	
	public static Vec2i operator +(Vec2i left, Vec2i right) {
		return new Vec2i(right.x + left.x, right.y + left.y);
	}
	
	public static Vec2i operator -(Vec2i left, Vec2i right) {
		return new Vec2i(right.x - left.x, right.y - left.y);
	}

	public override string ToString () {
		return string.Format("({0}, {1})", x, y);
	}
}