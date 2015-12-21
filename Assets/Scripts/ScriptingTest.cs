using UnityEngine;
using System.Collections;
using Jurassic;
using Jurassic.Library;

public class ScriptingTest : MonoBehaviour {

	ScriptEngine engine;

	void Start () {
		engine  = new ScriptEngine();
		engine.EnableExposedClrTypes = true; // You must enable this in order to use interop feaure.
		engine.SetGlobalFunction("hello", new System.Action<int>(Hello));
		engine.Execute("function fact(n) { return n === 0 ? 1 : n * fact(n - 1); } hello(fact(5));");
	}

	public void Hello (int result) {
		Debug.Log("Hello from C#: " + result);
	}
}
