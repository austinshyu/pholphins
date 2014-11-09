using UnityEngine;
using System.Collections;

public class DialogueGUI : MonoBehaviour {
	Dialogue current;
	GUISkin style;

	// Use this for initialization
	void Start () {
		current = null;
		style = Resources.Load<GUISkin>("GUISkins/overlaySkin");
	}
	
	void OnGUI() {
		if (current != null) {
			GUILayout.BeginArea(new Rect(30f, 30f, Screen.width/2f - 60f, Screen.height - 60f));
			GUILayout.Label(current.Text, style.label);
			GUILayout.EndArea ();
			GUILayout.BeginArea(new Rect(Screen.width/2f + 30f, 30f, Screen.width/2f - 60f, Screen.height - 60f));
			GUILayout.Label(GenChoicesString(), style.label);
			GUILayout.EndArea ();
		}
	}

	public void StartDialogue(Dialogue d) {
		SetDialogue(d);
		d.EnterDialogue();
	}

	/** Select a dialogue option from the active dialogue. Returns true if the dialogue is still active. */
	public bool SelectDialogueOption(int optionIndex) {
		Debug.Log ("Select option " + optionIndex);
		if (current == null) {
			return false;
		}
		bool stillUp = current.SelectOption(optionIndex);
		if (!stillUp) {
			current = null;
			return false;
		}
		return true;
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

	string GenChoicesString() {
		if (current == null) {
			return "";
		}
		string result = "";
		for (int i = 0; i < current.TransitionText.Length; i++) {
			result += (i + 1).ToString() + " : " + current.TransitionText[i] + "\n\n";
		}
		return result;
	}

	public bool Active { get { return current != null; } }
	public string ActiveText { get { return current.Text; } }
	public string[] ActiveOptions { get { return current.TransitionText; } }
}
