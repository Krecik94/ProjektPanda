using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private Rigidbody rb;
	private float moveVertical;
	private float moveHorizontal;
	private float mouseSpeedX;
	private float mouseSpeedY;
	private Event mouseEvent;
	private Vector3 mousePosition;
	private Vector3 oldMousePosition;
	private Vector3 lockPosition;
	private float nextTreeSpawn = 5.0f;

	public float speedY;
	public float speedX;
	public float speedXThreshold;
	public Text mouseDelta;
	public Text pandaYSpeed;
	public float interpolation;
	public GameObject Background;

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
		
	}

	void SetMouseDelta () {
		mouseSpeedX = mouseEvent.delta.x;
		mouseSpeedY = mouseEvent.delta.y;
		mouseDelta.text = mouseSpeedX.ToString() + " , " + mouseSpeedY.ToString();
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Bamboo")) {
			rb.velocity = new Vector3(0, rb.velocity.y, 0);
			transform.position = Vector3.Lerp(transform.position, new Vector3(other.transform.position.x, transform.position.y, transform.position.z) , interpolation);
			mousePosition = Input.mousePosition;
			rb.useGravity = true;
			if(Input.GetKey(KeyCode.Mouse0)){
				moveVertical = (oldMousePosition.y - mousePosition.y) * speedY;
				moveHorizontal = (oldMousePosition.x - mousePosition.x) * speedX;
				if(moveVertical == 0 && moveHorizontal == 0) {
					rb.useGravity = false;
					rb.velocity = Vector3.zero;
				}
				else
					rb.velocity = new Vector3(moveHorizontal, moveVertical, 0);
				
			}
			pandaYSpeed.text = "Panda's Y velocity: " + rb.velocity.y.ToString();
			oldMousePosition = mousePosition;
			
		}
	}


	void OnTriggerStay(Collider other) 
    {
        if (other.gameObject.CompareTag ("Bamboo")) {	
        	mousePosition = Input.mousePosition;
			rb.useGravity = true;
			if(Input.GetKey(KeyCode.Mouse0)){
				moveVertical = (oldMousePosition.y - mousePosition.y) * speedY;
				moveHorizontal = (oldMousePosition.x - mousePosition.x) * speedX;
				if(moveVertical == 0 && moveHorizontal == 0) {
					rb.useGravity = false;
					rb.velocity = Vector3.zero;
				}
				else
					rb.velocity = new Vector3(moveHorizontal, moveVertical, 0);
				
			}
			pandaYSpeed.text = "Panda's Y velocity: " + rb.velocity.y.ToString();
			oldMousePosition = mousePosition;
			if(moveHorizontal < speedXThreshold)
				transform.position = Vector3.Lerp(transform.position, new Vector3(other.transform.position.x, transform.position.y, transform.position.z) , interpolation);
        }
    }
}
