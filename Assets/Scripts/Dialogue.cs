using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Dialogue : MonoBehaviour {
	Global global;
	public TextAsset contents;
	JSONObject json;
	int currentNode;
	string nodeText;
	string[] nodeTransitionText;
	int[] nodeTransitions;

	// Use this for initialization
	void Start () {
		global = FindObjectOfType<Global>();
		json = new JSONObject(contents.ToString());
		EnterDialogue();
		/*Debug.Log(json.ToString());
		Debug.Log(json.GetField("nodes"));
		Debug.Log (currentNode);
		foreach (string s in nodeTransitionText) {
			//Debug.Log(s);
		}*/
	}
	
	// Update is called once per frame
	void Update () {
		json = new JSONObject(contents.ToString());
		if(Input.GetKeyDown (KeyCode.A)){
			currentNode+=1;
			Load (currentNode);
		}
	}

	public void EnterDialogue () {
		foreach(JSONObject obj in json.GetField("nodes").list) {
			if (obj.HasField("startIf")) {
				if (SatisfiesConditions(obj.GetField("startIf").list)) {
					Load((int) obj.GetField("node").n);
					return;
				}
			}
		}
		Load((int) json.GetField("nodes")[0].GetField ("node").n);
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
		if (nodeTransitions[optionIndex] == -1) {
			Load (-1);
			return false;
		}
		Debug.Log(nodeTransitions[optionIndex]);

		Load (nodeTransitions[optionIndex]);
		return true;
	}

	void Load(int node) {
		currentNode = node;
		JSONObject jsonNode = FindNode(currentNode);
		if (jsonNode == null) {
			nodeText = "";
			nodeTransitionText = new string[0];
			nodeTransitions = new int[0];
			return;
		}
		nodeText = jsonNode.GetField("text").str;
		int count = jsonNode.GetField("transitions").Count;

		List<int> tempNodeTransitions = new List<int>();
		List<string> tempNodeTransitionText = new List<string>();
		for (int i = 0; i < count; i++) {
			List<JSONObject> transitionList = jsonNode.GetField("transitions")[i].list;
			// Manage conditions if they exist. Skip this node if they are not satisfied.
			if (transitionList.Count > 2) {
				List<JSONObject> conditions = transitionList[2].list;
				if (!SatisfiesConditions(conditions)) {
					continue;
				}
			}
			tempNodeTransitionText.Add(transitionList[0].str);
			if (jsonNode.GetField("transitions")[i][1].IsString) {
				if (jsonNode.GetField("transitions")[i][1].str != "exit") {
					Debug.LogError("Got string transition that is not \"exit\".");
				}
				tempNodeTransitions.Add (-1);
			} else {
				tempNodeTransitions.Add ((int) transitionList[1].n);
			}
		}

		nodeTransitions = tempNodeTransitions.ToArray();
		nodeTransitionText = tempNodeTransitionText.ToArray();
		if (jsonNode.HasField("set")) {
			SetConditions(jsonNode.GetField("set").list);
		}
	}

	JSONObject FindNode(int nodeID) {
		if (nodeID == -1) {
			return null;
		}
		foreach (JSONObject obj in json.GetField("nodes").list) {
			if ((int) obj.GetField("node").n == nodeID) {
				return obj;
			}
		}
		return null;
	}

	/** Returns whether global conditions are satisfied in LISTOFCONDITIONS of the form "condition value". */
	bool SatisfiesConditions(List<JSONObject> listOfConditions) {
		if (listOfConditions.Count == 0) {
			return true;
		}
		foreach (JSONObject condObj in listOfConditions) {
			string[] condTokens = condObj.str.Split(new char[] {' '});
			if (global.ValueOfKey(condTokens[0]) != condTokens[1]) {
				return false;
			}
		}
		return true;
	}

	/** Sets global conditions from LISTOFCONDITIONS with elements of the form "condition newvalue".*/
	void SetConditions(List<JSONObject> listOfConditions) {
		if (listOfConditions.Count == 0) {
			return;
		}
		foreach (JSONObject condObj in listOfConditions) {
			string[] condTokens = condObj.str.Split(new char[] {' '});
			global.SetValueOfKey(condTokens[0], condTokens[1]);
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
