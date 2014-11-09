using UnityEngine;
using System.Collections;

public class mainchar : MonoBehaviour
{
	public float moveSpeed = 2.0f;
	private Animator anim;
	public bool facingLeft = true;
	private Transform _transform;
	public Animator walking;
	//public bool idle;
	public Vector3 sitLocation = Vector3.zero;
	public Vector3 lastLocation = Vector3.zero;
	public bool canSit = false;
	public bool sitting = false;
	public Dialogue availableDialogue = null;

	Global global;
	// Use this for initialization
	void Start ()
	{
		anim = this.GetComponent<Animator> ();
		_transform = this.GetComponent<Transform> ();
		global = FindObjectOfType<Global>();
	}

	// Update is called once per frame
	void Update ()
	{
		rigidbody.freezeRotation = true;
		if (!sitting && this.gameObject.rigidbody.velocity != Vector3.zero) {
			anim.SetBool ("walking", true);
			//this.gameObject.GetComponent<Animator>().Play (1);
		} else if (!sitting && this.gameObject.rigidbody.velocity == Vector3.zero) {
			anim.SetBool ("walking", false);	
			this.gameObject.GetComponent<Animator> ().Play ("person_idle");

		}
		if (sitting) {
			this.gameObject.GetComponent<SpriteRenderer> ().sprite = ((Sprite)(Resources.Load<Sprite> ("person_sit")));		
		}

		// Disallow movement while in conversation
		if (global.DialogueGUI.Active) {
			this.gameObject.rigidbody.velocity = new Vector3(0f, 0f, 0f);
		} else if (!sitting && Input.GetKey (KeyCode.LeftArrow)) {
			this.gameObject.rigidbody.velocity = new Vector3 (-moveSpeed, 0, 0);
			if (!facingLeft) {
				_transform.localScale = new Vector3 (-1 * _transform.localScale.x, _transform.localScale.y, _transform.localScale.z);
				facingLeft = true;
			}
		} else if (!sitting && Input.GetKey (KeyCode.RightArrow)) {
			this.gameObject.rigidbody.velocity = new Vector3 (moveSpeed, 0, 0);
			if (facingLeft) {
				_transform.localScale = new Vector3 (-1 * _transform.localScale.x, _transform.localScale.y, _transform.localScale.z);
				facingLeft = false;
			}
		} else if (!sitting && Input.GetKey (KeyCode.UpArrow)) {
			this.gameObject.rigidbody.velocity = new Vector3 (0, 0, moveSpeed);
		} else if (!sitting && Input.GetKey (KeyCode.DownArrow)) {
			this.gameObject.rigidbody.velocity = new Vector3 (0, 0, -moveSpeed);
		} else {
			this.gameObject.rigidbody.velocity = new Vector3 (0f, 0, 0);
		}

		if (!global.DialogueGUI.Active && !sitting && canSit && Input.GetKeyDown (KeyCode.Alpha1)) {
			if (!facingLeft) {
				_transform.localScale = new Vector3 (-1 * _transform.localScale.x, _transform.localScale.y, _transform.localScale.z);
				facingLeft = true;
			}
			Sit();
		} else if (!global.DialogueGUI.Active && sitting && Input.GetKeyDown (KeyCode.Alpha1)) {
			Stand();
		}

		// Separate logic for starting conversations
		if (!global.DialogueGUI.Active && availableDialogue != null && Input.GetKeyDown(KeyCode.Alpha1)) {
			global.DialogueGUI.StartDialogue(availableDialogue);
		} else if (global.DialogueGUI.Active) {
			bool stillActive = true;
			int numActiveOptions = global.DialogueGUI.ActiveOptions.Length;
			if (Input.GetKeyDown(KeyCode.Alpha1)) {
				if (numActiveOptions >= 1) {
					stillActive = global.DialogueGUI.SelectDialogueOption(0);
				}
			} else if (Input.GetKeyDown(KeyCode.Alpha2)) {
				if (numActiveOptions >= 2) {
					stillActive = global.DialogueGUI.SelectDialogueOption(1);
				}
			} else if (Input.GetKeyDown(KeyCode.Alpha3)) {
				if (numActiveOptions >= 3) {
					stillActive = global.DialogueGUI.SelectDialogueOption(2);
				}
			} else if (Input.GetKeyDown(KeyCode.Alpha4)) {
				if (numActiveOptions >= 4) {
					stillActive = global.DialogueGUI.SelectDialogueOption(3);
				}
			}

			if (!stillActive && sitting) {
				Stand ();
			}
		}
	}

	void Sit ()
	{
		this.gameObject.GetComponent<BoxCollider> ().enabled = false;
		this.gameObject.GetComponent<Animator> ().enabled = false;
		lastLocation = this.transform.position;
		this.transform.position = sitLocation;
		this.gameObject.GetComponent<SpriteRenderer> ().sprite = ((Sprite)(Resources.Load<Sprite> ("person_sit")));
		sitting = true;
	}

	void Stand ()
	{
		this.gameObject.GetComponent<BoxCollider> ().enabled = true;
		this.gameObject.GetComponent<Animator> ().enabled = true;
		sitting = false;
		this.transform.position = lastLocation;
		//this.gameObject.GetComponent<SpriteRenderer> ().sprite = ((Sprite)(Resources.Load<Sprite>("person_spriteplane_0")));
		sitLocation = Vector3.zero;
	}
}
