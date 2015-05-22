using UnityEngine;
using System.Collections;

public class Climbing : MonoBehaviour {

	private enum Location {LEFT, MIDDLE, RIGHT};
	private Location vine;
	private float nextVineChange = 0.0f;

	public float speed;
	public float vineChangeCooldown = 1.0f;

	// Use this for initialization
	void Start () {
		vine = Location.MIDDLE;
	}

	void FixedUpdate () {
		float moveVer = Input.GetAxis ("Vertical");
		float moveHor = Input.GetAxis ("Horizontal");
		float leftOrRight = 0;
		if (moveVer < 0 && transform.position.y < 0.7f)
			moveVer = 0;
		if (moveHor > 0 && vine != Location.RIGHT && Time.time > nextVineChange) {
			nextVineChange = Time.time + vineChangeCooldown;
			leftOrRight = 1;
			vine += 1;
		} else if (moveHor < 0 && vine != Location.LEFT && Time.time > nextVineChange) {
			nextVineChange = Time.time + vineChangeCooldown;
			leftOrRight = -1;
			vine -= 1;
		}

		Vector3 movement = new Vector3 (leftOrRight, speed * moveVer, 0);
		transform.Translate (movement);
	}
}
