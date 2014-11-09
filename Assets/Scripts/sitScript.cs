using UnityEngine;
using System.Collections;

public class sitScript : MonoBehaviour {
	Vector3 sitLocation;
	GameObject bubble;
	//public bool setFalse = true;
	// Use this for initialization
	void Start () {
		sitLocation = transform.parent.FindChild("sit_location").position;
		bubble = transform.parent.FindChild("talk").gameObject;
		bubble.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerStay(Collider collide){
		print ("can sit" + gameObject.name);
		if (collide.gameObject.name == "person") {
			//setFalse = false;
			mainchar player = collide.gameObject.GetComponent<mainchar>();
			player.canSit = true;
			player.sitLocation = sitLocation;
			bubble.SetActive(true);
		}
	}

	void OnTriggerExit(Collider collide){
		if (collide.gameObject.name == "person") {
			//setFalse = false;
			/*collide.gameObject.GetComponent<mainchar>().canSit = true;
			if (this.gameObject.name == "next_to_creepy") {
				collide.gameObject.GetComponent<mainchar> ().sitLocation = new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z - 0.7f); 
			}*/
			collide.gameObject.GetComponent<mainchar>().canSit = false;
			bubble.SetActive(false);
		}
	}
}
