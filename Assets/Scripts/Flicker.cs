using UnityEngine;
using System.Collections;

public class Flicker : MonoBehaviour {
	Light l;
	float originalIntensity;
	public float severity;
	public float probability;

	int counter;
	// Use this for initialization
	void Start () {
		l = GetComponent<Light>();
		originalIntensity = l.intensity;
		counter = 0;
	}
	
	// Update is called once per frame
	void Update () {
	 	if (counter <= 0) {
			l.intensity = originalIntensity;
			if (Random.Range(0f, 1f) < probability) {
				counter = 5 + (int) Random.Range(0f, 10f);
				l.intensity = originalIntensity * (1f - severity);
			}
		} else {
			counter--;
		}
	}
}
