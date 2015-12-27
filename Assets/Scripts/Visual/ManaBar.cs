using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Mana))]
public class ManaBar : GenericBar {

	Mana mana;

	void Start() {
		Init();
		mana = GetComponent<Mana>();
	}

	protected override float Value() {
		return mana.value;
	}

	protected override float MaxValue() {
		return mana.maxValue;
	}

	protected override Vector2 Offset() {
		return new Vector2(-20, -38);
	}

	protected override Color ForegroundColour() {
		return Color.blue;
	}
}
