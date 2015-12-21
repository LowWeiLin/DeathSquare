using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	
	EntityBase entity;
	
	public int maxHp = 10;
	public int hp = 10;
	
	// Use this for initialization
	void Start () {
		entity = GetComponent<EntityBase> ();
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
			entity.DestroyEntity();
		}
	}
	
	public void Heal(int amount)
	{
		hp += amount;
		hp = Mathf.Min (hp, maxHp);
	}
	
}