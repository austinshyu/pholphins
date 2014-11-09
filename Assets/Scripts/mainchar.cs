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
		// Use this for initialization
		void Start ()
		{
				anim = this.GetComponent<Animator> ();
				_transform = this.GetComponent<Transform> ();
		}
	
		// Update is called once per frame
		void Update ()
		{
				rigidbody.freezeRotation = true;
				if (!sitting && this.gameObject.rigidbody.velocity != Vector3.zero) {
						anim.SetBool ("walking", true);
						//this.gameObject.GetComponent<Animator>().Play (1);
				} else if (!sitting && this.gameObject.rigidbody.velocity == Vector3.zero) {
						print ("blech");
						anim.SetBool ("walking", false);	
						this.gameObject.GetComponent<Animator> ().Play ("person_idle");

				}
				if (sitting) {
						this.gameObject.GetComponent<SpriteRenderer> ().sprite = ((Sprite)(Resources.Load<Sprite> ("person_sit")));		
				}
				if (!sitting && Input.GetKey (KeyCode.LeftArrow)) {
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
				if (!sitting && canSit && Input.GetKeyDown (KeyCode.Alpha1)) {
						if (!facingLeft) {
								_transform.localScale = new Vector3 (-1 * _transform.localScale.x, _transform.localScale.y, _transform.localScale.z);
				facingLeft = true;
						}
						this.gameObject.GetComponent<BoxCollider> ().enabled = false;
						this.gameObject.GetComponent<Animator> ().enabled = false;
						lastLocation = this.transform.position;
						this.transform.position = sitLocation;
						this.gameObject.GetComponent<SpriteRenderer> ().sprite = ((Sprite)(Resources.Load<Sprite> ("person_sit")));
						sitting = true;
				} else if (sitting && Input.GetKeyDown (KeyCode.Alpha1)) {
						this.gameObject.GetComponent<BoxCollider> ().enabled = true;
						this.gameObject.GetComponent<Animator> ().enabled = true;
						sitting = false;
						this.transform.position = lastLocation;
						//this.gameObject.GetComponent<SpriteRenderer> ().sprite = ((Sprite)(Resources.Load<Sprite>("person_spriteplane_0")));
						sitLocation = Vector3.zero;
				}



		}
}
