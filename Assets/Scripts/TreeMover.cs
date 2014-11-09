using UnityEngine;
using System.Collections;

public class TreeMover : MonoBehaviour {
	public float wrapWidth;
	public float xSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Transform[] childTransforms = gameObject.GetComponentsInChildren<Transform>();
		foreach (Transform t in childTransforms) {
			Vector3 moved = t.position + new Vector3(xSpeed, 0f, 0f);
			t.position = moved;
			Vector3 wrap = new Vector3(2f * wrapWidth, 0f, 0f);
			if (t.position.x < -wrapWidth) {
				t.position += wrap;
			} else if (t.position.x > wrapWidth) {
				t.position -= wrap;
			}
		}
	}
}
