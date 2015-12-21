using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	
	public int maxHp = 10;
	public int hp = 10;
	
	void Start () {
		hp = maxHp;
	}
	
	public void TakeDamage(int damage) {

		Visuals visuals = GetComponent<Visuals>();
		if (visuals != null) {
			visuals.FlashRed();
		}

		hp -= damage;
		hp = Mathf.Max (hp, 0);
		if (hp <= 0) {
//			Destroy(gameObject);
		}
	}
	
	public void Heal(int amount)
	{
		hp += amount;
		hp = Mathf.Min (hp, maxHp);
	}
	
}