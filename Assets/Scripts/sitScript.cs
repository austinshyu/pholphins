using UnityEngine;
using System.Collections;

public class sitScript : MonoBehaviour {
	//public bool setFalse = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}
	void OnTriggerStay(Collider collide){
		print ("can sit");
		if (collide.gameObject.name == "person") {
			//setFalse = false;
			collide.gameObject.GetComponent<mainchar>().canSit = true;
						if (this.gameObject.name == "next_to_creepy") {
								collide.gameObject.GetComponent<mainchar> ().sitLocation = new Vector3 (this.transform.position.x, 1.1f, this.transform.position.z - 0.7f); 
						}
						else if(this.gameObject.name == "next_to_madam"){
				collide.gameObject.GetComponent<mainchar> ().sitLocation = new Vector3 (3.9f, 1.1f, this.transform.position.z +0.25f);
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
			collide.gameObject.GetComponent<mainchar>().canSit = false;
		}
	}
}
