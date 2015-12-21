using UnityEngine;
using System.Collections;
using UniLua;

public class ScriptingTest : MonoBehaviour {

	ILuaState Lua;

	void Start () {
		Lua = LuaAPI.NewState();
		Lua.L_OpenLibs();
		Lua.L_DoString("function fact (n) if n == 0 then return 1 else return n * fact(n-1) end end print(fact(5))");
	}
}
