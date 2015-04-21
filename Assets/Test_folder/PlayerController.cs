using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private Rigidbody rb;
	private float moveVertical;
	private float strafe;
	private float turn;

	public float speed;
	public float torque;

	// Use this for initialization
	void Start () {
		//rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate () {

		/*moveVertical = Input.GetAxis("Jump");
		strafe = Input.GetAxis("Horizontal");
		turn = Input.GetAxis("Vertical");
		Vector3 movement = new Vector3 (strafe, moveVertical, 0);
		rb.AddForce(movement * speed);
		rb.AddTorque(transform.up * torque * turn);*/
		
	}
}
