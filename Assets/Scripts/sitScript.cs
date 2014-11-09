using UnityEngine;
using System.Collections;

public class sitScript : MonoBehaviour {
	Global global;
	Vector3 sitLocation;
	GameObject bubble;
	Dialogue dlg;
	//public bool setFalse = true;
	// Use this for initialization
	void Start () {
		global = FindObjectOfType<Global>();
		sitLocation = transform.parent.FindChild("sit_location").position;
		bubble = transform.parent.FindChild("talk").gameObject;
		bubble.SetActive(false);
		dlg = transform.parent.GetComponent<Dialogue>();
	}
	
	// Update is called once per frame
	void Update () {
		if (global.DialogueGUI.Active) {
			bubble.SetActive(false);
		}
	}

	void OnTriggerStay(Collider collide){
		print ("can sit" + gameObject.name);
		if (collide.gameObject.name == "person") {
			//setFalse = false;
			mainchar player = collide.gameObject.GetComponent<mainchar>();
			player.canSit = true;
			player.sitLocation = sitLocation;
			player.availableDialogue = dlg;
			if (!global.DialogueGUI.Active) {
				bubble.SetActive(true);
			}
		}
	}

	void OnTriggerExit(Collider collide){
		if (collide.gameObject.name == "person") {
			//setFalse = false;
			/*collide.gameObject.GetComponent<mainchar>().canSit = true;
			if (this.gameObject.name == "next_to_creepy") {
				collide.gameObject.GetComponent<mainchar> ().sitLocation = new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z - 0.7f); 
			}*/
			mainchar player = collide.gameObject.GetComponent<mainchar>();
			player.canSit = false;
			player.availableDialogue = null;
			bubble.SetActive(false);
		}
	}
}
