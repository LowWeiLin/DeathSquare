using UnityEngine;
using System.Collections;

public class Visuals : MonoBehaviour {

	public GameObject model;

	public void FlashRed() {
		MeshRenderer renderer = model.GetComponent<MeshRenderer>();
		if (renderer != null) {
			StartCoroutine(ActuallyFlashRed(renderer));
		}
	}

	IEnumerator ActuallyFlashRed(MeshRenderer renderer) {
		renderer.material.color = Color.red;
		yield return new WaitForSeconds(0.1f);
		renderer.material.color = Color.white;
	}
}
