using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Global : MonoBehaviour {
	DialogueGUI dialogueGUI;
	public Dictionary<string,string> values = new Dictionary<string,string>();
	// Use this for initialization
	void Start () {
		dialogueGUI = GetComponent<DialogueGUI>();
		values.Add ("talkedToCreeper", "1");
		values.Add ("talkedToMadam", "0");
		values.Add ("talkedToBully", "0");
		values.Add ("talkedToMother", "0");
		values.Add ("hideCreeper", "0"); // hide creeper if talked to everyone once
		values.Add ("tookKnife", "0");

	}
	void Update(){
		Debug.Log(ValueOfKey ("talkedToCreeper"));
	}
	public string ValueOfKey(string key){
		string rv = "0";
		values.TryGetValue(key,out rv);
		return rv; 
	}
	public void SetValueOfKey(string key, string value) {
		values[key] = value;
	}
	public DialogueGUI DialogueGUI { get { return dialogueGUI; } }
}
