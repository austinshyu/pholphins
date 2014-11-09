using UnityEngine;
using System.Collections;

public class TreeMover : MonoBehaviour {
	public float wrapWidth;
	public float xSpeed;
	GameObject[] grounds = new GameObject[2];

	// Use this for initialization
	void Start () {
		grounds[0] = transform.FindChild("Ground1").gameObject;
		grounds[1] = transform.FindChild("Ground2").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		Transform[] childTransforms = gameObject.GetComponentsInChildren<Transform>();
		foreach (Transform t in childTransforms) {
			if (t.name != "tree") {
				continue;
			}
			Vector3 moved = t.position + new Vector3(xSpeed, 0f, 0f);
			t.position = moved;
			Vector3 wrap = new Vector3(2f * wrapWidth, 0f, 0f);
			if (t.position.x < -wrapWidth) {
				t.position += wrap;
			} else if (t.position.x > wrapWidth) {
				t.position -= wrap;
			}
		}

		float shiftTex = xSpeed / grounds[0].transform.lossyScale.x / 10f;
		foreach (GameObject ground in grounds) {
			ground.renderer.material.SetTextureOffset("_MainTex",
	  				ground.renderer.material.GetTextureOffset("_MainTex") + new Vector2(shiftTex, 0f));
		}
	}
}
