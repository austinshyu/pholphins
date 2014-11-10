using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Global : MonoBehaviour
{
		DialogueGUI dialogueGUI;
		public Dictionary<string,string> values = new Dictionary<string,string> ();
		public GameObject creeper;
		public GameObject madam;
		public GameObject bully;
		public GameObject mother;
		// Use this for initialization
		void Start ()
		{
				dialogueGUI = GetComponent<DialogueGUI> ();
				madam = GameObject.Find ("madam_sit");
				bully = GameObject.Find ("bully_sit");
				mother = GameObject.Find ("newmother_sit");
				values.Add ("talkedToCreeper", "0");
				values.Add ("talkedToMadam", "0");
				values.Add ("talkedToBully", "0");
				values.Add ("talkedToMother", "0");
				values.Add ("hideCreeper", "0"); // hide creeper if talked to everyone once
				values.Add ("tookKnife", "0");
				values.Add ("showCreeperAgain!", "0");
		values.Add ("end", "0");
				madam.SetActive (false);
				bully.SetActive (false);
				mother.SetActive (false);
		GameObject.Find ("knife").GetComponent<SpriteRenderer>().enabled = false;
		}

		void Update ()
		{
				Debug.Log (values ["talkedToCreeper"]);
				/*madam.SetActive (false);
				bully.SetActive (false);
				mother.SetActive (false);*/
				if (ValueOfKey ("hideCreeper") == "1") {
						print ("falafel");
						creeper.GetComponent<Dialogue> ().contents = Resources.Load <TextAsset> ("briefcase2");
						creeper.GetComponent<SpriteRenderer> ().enabled = false;
						GameObject.Find ("next_to_creepy").GetComponent<BoxCollider> ().enabled = true;
						EnableBubble (creeper);
						creeper.GetComponent<Dialogue> ().enabled = true;
						//GameObject.Find ("person").GetComponent<mainchar> ().canSit = true;
						GameObject.Find ("next_to_creepy").GetComponent<sitScript> ().enabled = true;
				}
				if (ValueOfKey ("tookKnife") == "1") {
						//GameObject.Find ("knife").GetComponent<SpriteRenderer>().enabled = true;
						print ("wow so many falafels");
						DisableBubble (creeper);
						GameObject.Find ("next_to_creepy").GetComponent<BoxCollider> ().enabled = false;
						creeper.GetComponent<Dialogue> ().enabled = false;
						//GameObject.Find ("person").GetComponent<mainchar> ().canSit = false;
						//GameObject.Find ("person").GetComponent<mainchar> ().availableDialogue = null;
						GameObject.Find ("next_to_creepy").GetComponent<sitScript> ().enabled = false;

				}
				if (ValueOfKey ("talkedToCreeper") == "1") {
						DisableBubble (creeper);
						GameObject.Find ("next_to_creepy").GetComponent<BoxCollider> ().enabled = false;
						creeper.GetComponent<Dialogue> ().enabled = false;
						//GameObject.Find ("person").GetComponent<mainchar> ().canSit = false;
						//GameObject.Find ("person").GetComponent<mainchar> ().availableDialogue = null;
						GameObject.Find ("next_to_creepy").GetComponent<sitScript> ().enabled = false;

						madam.SetActive (true);
						//madam.
						bully.SetActive (true);
						mother.SetActive (true);
				} 
				if (ValueOfKey ("talkedToMadam") == "1") {
						DisableBubble (madam);
						GameObject.Find ("next_to_madam").GetComponent<BoxCollider> ().enabled = false;
				}
				if (ValueOfKey ("talkedToMother") == "1") {
						DisableBubble (mother);
						GameObject.Find ("next_to_mom").GetComponent<BoxCollider> ().enabled = false;
				}
				if (ValueOfKey ("talkedToBully") == "1") {
						DisableBubble (bully);
						GameObject.Find ("next_to_bully").GetComponent<BoxCollider> ().enabled = false;
				}
				if (ValueOfKey ("talkedToBully") == "1" && ValueOfKey ("talkedToMother") == "1" && ValueOfKey ("talkedToMadam") == "1" && ValueOfKey ("talkedToCreeper") == "1") {
						SetValueOfKey ("hideCreeper", "1");
						SetValueOfKey ("talkedToCreeper", "0");
				}
				if (ValueOfKey ("end") == "1") {
			GameObject.Find ("cop_sit").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("cop_blood");
			DisableBubble (GameObject.Find ("cop_sit"));
			GameObject.Find ("next_to_cop").GetComponent<BoxCollider>().enabled = false;
			creeper.SetActive (false);
			madam.SetActive (false);
			mother.SetActive (false);
			bully.SetActive (false);
		}
			
		}

		public void SetValueOfKey (string key, string value)
		{
				values [key] = value;
		}

		public void DisableBubble (GameObject obj)
		{
				obj.transform.FindChild ("talk").GetComponent<SpriteRenderer> ().enabled = false;
		}

		public void EnableBubble (GameObject obj)
		{
				obj.transform.FindChild ("talk").GetComponent<SpriteRenderer> ().enabled = true;
		
		}

		public string ValueOfKey (string key)
		{
				string rv = "0";
				values.TryGetValue (key, out rv);
				return rv; 
		}

		public DialogueGUI DialogueGUI { get { return dialogueGUI; } }
}