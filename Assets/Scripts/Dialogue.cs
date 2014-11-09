using UnityEngine;
using System.Collections;

public class Dialogue : MonoBehaviour {
	public TextAsset contents;
	JSONObject json;
	int currentNode;
	string nodeText;
	string[] nodeTransitionText;
	string[] nodeTransitions;

	// Use this for initialization
	void Start () {
		json = new JSONObject(contents.ToString());
		EnterDialogue();
		Debug.Log(json.ToString());
		Debug.Log(json.GetField("nodes"));
		Debug.Log (currentNode);
		foreach (string s in nodeTransitionText) {
			Debug.Log(s);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown (KeyCode.A)){
			currentNode+=1;
			Load (currentNode);
		}
	}

	public void EnterDialogue () {
		int node = (int) json.GetField("nodes")[0].GetField ("node").n;
		Load(node);
	}

	public void ExitDialogue () {
		Load(-1);
	}

	/** Select the text option at OPTIONINDEX. Returns whether the dialog is still active. */
	public bool SelectOption(int optionIndex) {
		if (optionIndex < 0 || optionIndex >= nodeTransitions.Length) {
			Debug.LogWarning("Transitioned to unknown option index: " + optionIndex.ToString());
			Load(-1);
			return false;
		}
		if (nodeTransitions[optionIndex] == "exit") {
			Load (-1);
			return false;
		}
		Debug.Log(nodeTransitions[optionIndex]);

		Load (int.Parse(nodeTransitions[optionIndex]));
		return true;
	}

	void Load(int node) {
		currentNode = node;
		if (node == -1) {
			nodeText = "";
			nodeTransitionText = new string[0];
			nodeTransitions = new string[0];
			return;
		}
		JSONObject jsonNode = json.GetField("nodes")[currentNode];
		nodeText = jsonNode.GetField("text").str;
		int count = jsonNode.GetField("transitions").Count;
		nodeTransitions = new string[count];
		nodeTransitionText = new string[count];
		for (int i = 0; i < count; i++) {
			nodeTransitionText[i] = jsonNode.GetField("transitions")[i][0].str;
			if (jsonNode.GetField("transitions")[i][1].IsString) {
				nodeTransitions[i] = jsonNode.GetField("transitions")[i][1].str;
			} else {
				nodeTransitions[i] = jsonNode.GetField("transitions")[i][1].n.ToString();
			}
		}

	}

	public string Text {
		get { return nodeText; }
	}

	public string[] TransitionText {
		get {
			return nodeTransitionText;
		}
	}
}
