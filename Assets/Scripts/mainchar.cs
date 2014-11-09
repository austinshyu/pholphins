using UnityEngine;
using System.Collections;

public class mainchar : MonoBehaviour {
	public float moveSpeed = 2.0f;
	private Animator anim;
	public bool facingLeft = true;
	private Transform _transform;
	public Animator walking;
	public bool idle;
	// Use this for initialization
	void Start () {
		anim = this.GetComponent<Animator>();
		_transform = this.GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		rigidbody.freezeRotation = true;
		if (this.gameObject.rigidbody.velocity != Vector3.zero) {
						anim.SetBool ("walking", true);
			//this.gameObject.GetComponent<Animator>().Play (1);
				} else if (this.gameObject.rigidbody.velocity == Vector3.zero) {
			anim.SetBool ("walking",false);	
			this.gameObject.GetComponent<Animator>().Play (0);

		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			this.gameObject.rigidbody.velocity = new Vector3 (-moveSpeed, 0, 0);
			if(!facingLeft){
				_transform.localScale = new Vector3(-1 * _transform.localScale.x, _transform.localScale.y, _transform.localScale.z);
				facingLeft = true;
			}
		} else if(Input.GetKey (KeyCode.RightArrow)){
			this.gameObject.rigidbody.velocity = new Vector3(moveSpeed,0,0);
			if(facingLeft){
				_transform.localScale = new Vector3(-1 * _transform.localScale.x, _transform.localScale.y, _transform.localScale.z);
				facingLeft = false;
			}
		} else if(Input.GetKey (KeyCode.UpArrow)){
			this.gameObject.rigidbody.velocity = new Vector3(0,0,moveSpeed);
		} else if(Input.GetKey (KeyCode.DownArrow)){
			this.gameObject.rigidbody.velocity = new Vector3(0,0,-moveSpeed);
		} else {
			this.gameObject.rigidbody.velocity = new Vector3(0f,0,0);
		}
	}
}
