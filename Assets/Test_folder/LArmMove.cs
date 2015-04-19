using UnityEngine;
using System.Collections;

public class LArmMove : MonoBehaviour {

	private Rigidbody rb;
	private Vector3 mouse;
	private float control;
	private float move;

	public float speed;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		control = Input.GetAxis("Fire1");
		mouse = Input.mousePosition;
		move = (mouse.y- (Screen.height/2))-transform.position.y;
		if (control == 0){
			rb.AddForceAtPosition(move/10 * Vector3.up,
								  new Vector3(transform.position.x,
								  			  transform.position.y,
								  			  transform.position.z + 0.05f),
								  ForceMode.Impulse);
		}
		else if(control != 0) {
			if(move > 10)
				rb.AddForce(10 * Vector3.up, ForceMode.Impulse);
			else
				rb.AddForce(move * Vector3.up, ForceMode.Impulse);
		}
	}
}