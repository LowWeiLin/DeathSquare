using UnityEngine;
using System.Collections;
using System;

public class Util {
	
	public static void Assert(bool condition) {
		Assert(condition, "invariant violation");
	}

	public static void Assert(bool condition, string message) {
		if (!condition) {
			throw new Exception("Assertion failure: " + message);
		}
	}
}
