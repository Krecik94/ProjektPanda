using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private Rigidbody rb;
	private float[] moveVertical;
	private float[] moveHorizontal;
	//private float mouseSpeedX;
	private float tempPandaYPosition;
	private Event mouseEvent;
	private Vector3 mousePosition;
	private Vector3 oldMousePosition;
	private Vector3 lockPosition;
	private float LPMDownTime;
	private float LMPDownWait;

	public float speedY;
	public float speedYMax;
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
		lockPosition = transform.position;
		tempPandaYPosition = 0f;
		moveHorizontal = new float[5];
		moveVertical = new float[5];
		LMPDownWait = 0.2f;
		pandaYSpeed.text = "Panda's Y velocity: " + rb.velocity.y.ToString();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void FixedUpdate () {
		SetMouseDelta();
		pandaYSpeed.text = "Panda's Y velocity: " + rb.velocity.y.ToString();
		
	}

	void SetMouseDelta () {
		mouseDelta.text = (Screen.height/2).ToString() + " , " + Input.mousePosition.y.ToString();
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Bamboo")) {
			rb.velocity = new Vector3(0, rb.velocity.y, 0);
			transform.position = Vector3.Lerp(transform.position, new Vector3(other.transform.position.x, transform.position.y, transform.position.z) , interpolation);
			lockPosition = transform.position;
			PandaMove();
		}
		else if (other.gameObject.CompareTag ("Vines")) {

		}
	}


	void OnTriggerStay(Collider other) {
        if (other.gameObject.CompareTag ("Bamboo")) {	
        	PandaMove();
			if(moveHorizontal[0] < speedXThreshold)
				
				transform.position = Vector3.Lerp(transform.position, new Vector3(other.transform.position.x, transform.position.y, transform.position.z) , interpolation);
        }
    }

    void PandaMove() {
    	if(Input.GetMouseButtonDown(0)) {
   			LPMDownTime = Time.time + LMPDownWait;
			lockPosition = transform.position;
    	}

    	mousePosition = Input.mousePosition;
		rb.useGravity = true;
		
		if(Input.GetKey(KeyCode.Mouse0)){
			rb.useGravity = false;
			rb.velocity = Vector3.zero;
			if(LPMDownTime < Time.time) {
				tempPandaYPosition = - mousePosition.y + Screen.height/2;
				tempPandaYPosition /= Screen.height;
				tempPandaYPosition += lockPosition.y;

				if(tempPandaYPosition  - 0.7 >= 0)
					transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, tempPandaYPosition, transform.position.z) , interpolation);
				else
					transform.position = lockPosition;
			}

			moveVertical[0] = ((oldMousePosition.y - mousePosition.y)/Screen.height) * speedY;
			moveHorizontal[0] = ((oldMousePosition.x - mousePosition.x)/Screen.height) * speedX;
			for(int i = 0; i<4; ++i) {
				moveVertical[i+1] = moveVertical[i];
				moveHorizontal[i+1] = moveHorizontal[i];
			}	
		}

		if(Input.GetMouseButtonUp(0)){

			for(int i = 1; i<5; ++i){

				if(Mathf.Abs(moveVertical[0]) < Mathf.Abs(moveVertical[i]))
					moveVertical[0] = moveVertical[i];

				if(Mathf.Abs(moveHorizontal[0]) < Mathf.Abs(moveHorizontal[i]))
					moveHorizontal[0] = moveHorizontal[i];
			}
			if(moveVertical[0] > speedYMax)
				moveVertical[0] = speedYMax;
			if(moveHorizontal[0] > 5)
				moveHorizontal[0] = 5;
			rb.velocity = new Vector3(moveHorizontal[0], moveVertical[0], 0);	
		}
		oldMousePosition = mousePosition;

    }
}
