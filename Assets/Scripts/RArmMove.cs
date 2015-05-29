using UnityEngine;
using System.Collections;

public class RArmMove : MonoBehaviour {

	private Rigidbody rb;
	private float move;
	private float control;

	public float speed;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		control = Input.GetAxis("Fire2");
		if(control != 0) {
			move = Input.GetAxis("Mouse Y");
			rb.AddForce(-move * speed * transform.TransformVector(0, 1, 0));
		}
	}
}
