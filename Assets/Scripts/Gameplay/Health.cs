using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	
	public int maxValue = 10;
	public int value = 10;

	Maybe<Visuals> visuals;

	void Start () {
		value = maxValue;
		visuals = GetComponent<Visuals>();
	}
	
	public void TakeDamage(int damage) {

		visuals.IfPresent(v => v.FlashRed());

		value -= damage;
		value = Mathf.Max (value, 0);
		if (value <= 0) {

			GameController.Instance.UnregisterUnit(gameObject);
			GameController.Instance.UnregisterEntity(gameObject);

			if (gameObject.GetComponentInChildren<Camera>() != null) {
				gameObject.GetComponentInChildren<Camera>().transform.parent = null;
			}

			Destroy(gameObject);
		}
	}
	
	public void Heal(int amount)
	{
		value += amount;
		value = Mathf.Min (value, maxValue);
	}
	
}