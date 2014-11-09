using UnityEngine;
using System.Collections;

public class DialogueGUI : MonoBehaviour {
	Dialogue current;

	// Use this for initialization
	void Start () {
		current = null;
	}
	
	void OnGUI() {
		if (current != null) {
			GUI.Box (new Rect(0, 0, Screen.width, Screen.height), "");
		}
	}

	void SetDialogue(Dialogue d) {
		if (current == null) {
			current = d;
		}
	}

	void ClearDialogue(Dialogue d) {
		if (current == d) {
			current = null;
		}
	}
}
