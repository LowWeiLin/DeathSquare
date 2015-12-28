using UnityEngine;
using System.Collections;

public class Mana : MonoBehaviour {

	public int maxValue = 100;
	public int value = 100;
	public int regen = 4;

	void Start () {
		StartCoroutine(Regenerate());
	}

	IEnumerator Regenerate() {
		while (true) {
			Restore(regen);
			yield return new WaitForSeconds(1f);
		}
	}

	public void Consume(int amount) {
		value -= amount;
		value = Mathf.Max (value, 0);
	}

	public void Restore(int amount) {
		value += amount;
		value = Mathf.Min (value, maxValue);
	}

}