using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private Rigidbody rb;
	private float moveVertical;
	private float mouseSpeedX;
	private float mouseSpeedY;
	private Event mouseEvent;
	private Vector3 mousePosition;
	private Vector3 oldMousePosition;
	private Vector3 lockPosition;

	public float speed;
	public Text mouseDelta;
	public Text pandaYSpeed;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		oldMousePosition = mousePosition = Vector3.zero;
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
		mousePosition = Input.mousePosition;
		rb.useGravity = true;
		if(Input.GetKey(KeyCode.Mouse0)){
			moveVertical = (oldMousePosition.y - mousePosition.y) * speed;
			if(moveVertical == 0) {
				rb.useGravity = false;
				rb.velocity = Vector3.zero;
			}
			else {
				rb.velocity = new Vector3(0, moveVertical, 0);
			}
		}
		pandaYSpeed.text = "Panda's Y velocity: " + rb.velocity.y.ToString();
		oldMousePosition = mousePosition;
	}

	void SetMouseDelta () {
		mouseSpeedX = mouseEvent.delta.x;
		mouseSpeedY = mouseEvent.delta.y;
		mouseDelta.text = mouseSpeedX.ToString() + " , " + mouseSpeedY.ToString();
	}
}
