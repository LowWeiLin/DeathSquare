using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	
	public int maxHp = 10;
	public int hp = 10;
	
	void Start () {
		hp = maxHp;
	}
	
	public void TakeDamage(int damage) {

		Maybe<Visuals> visuals = GetComponent<Visuals>();
		visuals.IfPresent(v => v.FlashRed());

		hp -= damage;
		hp = Mathf.Max (hp, 0);
		if (hp <= 0) {

			GameObject.Find("GameController").GetComponent<GameController>().UnregisterUnit(gameObject);
			GameObject.Find("GameController").GetComponent<GameController>().UnregisterEntity(gameObject);

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