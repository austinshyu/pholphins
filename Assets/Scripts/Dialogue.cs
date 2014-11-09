using UnityEngine;
using System.Collections;

public class Dialogue : MonoBehaviour {
	public TextAsset contents;
	JSONObject json;
	int currentNode;
	string nodeText;
	string[] nodeTransitionText;
	int[] nodeTransitions;

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
		print ("this is current node: " + currentNode);
		print (nodeText);
	}

	public void EnterDialogue () {
		int node = (int) json.GetField("nodes")[0].n;
		Load(node);
	}

	public void ExitDialogue () {
		Load(-1);
	}

	void Load(int node) {
		currentNode = node;
		if (node == -1) {
			nodeText = "";
			nodeTransitionText = new string[0];
			nodeTransitions = new int[0];
			return;
		}
		JSONObject jsonNode = json.GetField("nodes")[currentNode];
		nodeText = jsonNode.GetField("text").str;
		int count = jsonNode.GetField("transitions").Count;
		nodeTransitions = new int[count];
		nodeTransitionText = new string[count];
		for (int i = 0; i < count; i++) {
			nodeTransitionText[i] = jsonNode.GetField("transitions")[i][0].str;
			nodeTransitions[i] = (int) jsonNode.GetField("transitions")[i][1].n;
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
