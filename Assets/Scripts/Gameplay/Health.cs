using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	
	public int maxHp = 10;
	public int hp = 10;

	Maybe<Visuals> visuals;

	void Start () {
		hp = maxHp;
		visuals = GetComponent<Visuals>();
	}
	
	public void TakeDamage(int damage) {

		visuals.IfPresent(v => v.FlashRed());

		hp -= damage;
		hp = Mathf.Max (hp, 0);
		if (hp <= 0) {

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
		hp += amount;
		hp = Mathf.Min (hp, maxHp);
	}
	
}