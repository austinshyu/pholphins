using UnityEngine;
using System.Collections;

public class Global : MonoBehaviour {
	DialogueGUI dialogueGUI;

	// Use this for initialization
	void Start () {
		dialogueGUI = GetComponent<DialogueGUI>();
	}
	
	public DialogueGUI DialogueGUI { get { return dialogueGUI; } }
}
