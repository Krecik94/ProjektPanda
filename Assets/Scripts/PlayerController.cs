using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private Rigidbody rb;
	private float moveVertical;
	private float mouseSpeedX;
	private float mouseSpeedY;
	private Event mouseEvent;

	public float speed;
	public Text mouseDelta;
	public Text pandaYSpeed;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		mouseSpeedY = 0f;
		pandaYSpeed.text = "Panda's Y velocity: " + rb.velocity.y.ToString();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnGUI () {
		mouseEvent = Event.current;
		if(mouseEvent.isMouse)
			SetMouseDelta();
	}

	void FixedUpdate () {
		moveVertical = mouseSpeedY * speed;
		rb.AddForce(new Vector3(0, moveVertical, 0), ForceMode.VelocityChange);
		pandaYSpeed.text = "Panda's Y velocity: " + rb.velocity.y.ToString();
	}

	void SetMouseDelta () {
		mouseSpeedX = mouseEvent.delta.x;
		mouseSpeedY = mouseEvent.delta.y;
		mouseDelta.text = mouseSpeedX.ToString() + " , " + mouseSpeedY.ToString();
	}
}
