using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Health))]
public class HealthBar : GenericBar {

	Health health;

	void Start() {
		Init();
		health = GetComponent<Health>();
	}

	protected virtual Rect Size() {
		return new Rect(0, 0, 40, 4);
	}

	protected override float Value() {
		return health.value;
	}

	protected override float MaxValue() {
		return health.maxValue;
	}

	protected override Vector2 Offset() {
		return new Vector2(-20, -40);
	}

	protected override Color ForegroundColour() {
		return Color.green;
	}
}
