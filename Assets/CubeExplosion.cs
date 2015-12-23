using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CubeExplosion : MonoBehaviour {

	public GameObject cubeParticlePrefab;
	public int numParticles;
	public Color colorBase;
	public float rotationSpeed = 360*10;
	public float translationSpeed = 0.5f;
	public float ttl = 3f;
	public float particleScale = 0.03f;

	private Vector3 rotationAxis;

	private List<GameObject> particles = new List<GameObject>();
	private List<Vector3> direction = new List<Vector3>();
	private float originalTTL;

	void Start () {
		originalTTL = ttl;
		for (int i=0 ; i<numParticles ; i++) {
			GameObject p = (GameObject) Instantiate(cubeParticlePrefab, transform.position, Quaternion.identity);
			p.transform.parent = this.transform;
			p.transform.Rotate(Random.rotation.eulerAngles);
			p.transform.localScale *= particleScale;
			p.GetComponent<Renderer>().material.color = colorBase;

			particles.Add(p);
			direction.Add(Random.onUnitSphere);
		}

		rotationAxis = Random.onUnitSphere;
	}
	
	// Update is called once per frame
	void Update () {
		
		for (int i=0 ; i<particles.Count ; i++) {
			GameObject p = particles[i];
			// rotation is not even visible
			//p.transform.rotation *= Quaternion.AngleAxis(rotationSpeed * Time.deltaTime, Vector3.left);
			p.transform.position += translationSpeed * direction[i] * Time.deltaTime;
			p.transform.localScale *= ttl/originalTTL;
		}

		ttl -= Time.deltaTime;
		if (ttl < 0) {
			foreach (GameObject p in particles) {
				Destroy(p);
			}
			Destroy(this);
		}
	}
}
