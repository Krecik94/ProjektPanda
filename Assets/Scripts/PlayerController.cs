using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {


	private Rigidbody rb;
	private Animator anim;
	private float[] moveVertical;
	private float[] moveHorizontal;
	//private float mouseSpeedX;
	private Vector3 tempPandaPosition;
	private Event mouseEvent;
	private Vector3 mousePosition;
	private Vector3 oldMousePosition;
	private Vector3 mouseDrag;
	private Vector3 lockPosition;
	private Vector3 mouseLockPosition;
	private float LPMDownTime;
	private float LMPDownWait;
	private bool grabbed;

	public float speedY;
	public float speedYMax;
	public float speedX;
	public float speedXMax;
	public float speedXThreshold;
	public Text mouseDelta;
	public Text pandaYSpeed;
	public Text pandaXSpeed;
	public float interpolation;
	public GameObject Background;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		anim = GetComponent<Animator> ();
		oldMousePosition = mousePosition = Vector3.zero;
		lockPosition = transform.position;
		moveHorizontal = new float[15];
		moveVertical = new float[15];
		LMPDownWait = 0.2f;
		grabbed = false;
		pandaYSpeed.text = "Panda's Y velocity: " + rb.velocity.y.ToString();
		pandaXSpeed.text = "Panda's X velocity: " + rb.velocity.x.ToString();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void FixedUpdate () {
		anim.speed = Mathf.Abs((rb.velocity.y / 1.0f) );
		anim.SetFloat ("Speed", rb.velocity.y);
		rb.useGravity = true;
		if(rb.velocity.y > speedYMax)
			rb.velocity = new Vector3(rb.velocity.x, speedYMax, 0);
		if(rb.velocity.y < -speedYMax)
			rb.velocity = new Vector3(rb.velocity.x, -speedYMax, 0);
		if(rb.velocity.x > speedXMax)
			rb.velocity = new Vector3(speedXMax, rb.velocity.y, 0);
		if(rb.velocity.x < -speedXMax)
			rb.velocity = new Vector3(-speedXMax, rb.velocity.y, 0);
		SetMouseDelta();
		pandaYSpeed.text = "Panda's Y velocity: " + rb.velocity.y.ToString();
		pandaXSpeed.text = "Panda's X velocity: " + rb.velocity.x.ToString();
	

		
	}

	void SetMouseDelta () {
		mouseDelta.text = (Screen.height/2).ToString() + " , " + Input.mousePosition.y.ToString();
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Bamboo")) {
			lockPosition = transform.position;
			PandaMove(other);
		}
		else if (other.gameObject.CompareTag ("Vines")) {

		}
	}


	void OnTriggerStay(Collider other) {
        if (other.gameObject.CompareTag ("Bamboo")) {	
        	PandaMove(other);
        }
    }

    void OnTriggerExit(Collider other) {
    	if (other.gameObject.CompareTag ("Bamboo")) {	
        	rb.useGravity = true;
        	mouseLockPosition = mousePosition;
        }
    }

    void PandaMove(Collider other) {

    	mousePosition = Input.mousePosition;
		rb.useGravity = true;
    	if(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) {
   			LPMDownTime = Time.time + LMPDownWait;
			lockPosition = transform.position;
			mouseLockPosition = mousePosition;
			tempPandaPosition=transform.position;
			tempPandaPosition.y +=Input.mousePosition.y/Screen.height - 0.5f;
    	}

    	
		
		if(Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse1)){
			anim.SetBool("ClimbingRight",true);
			anim.Play("ClimbingRight",-1,Mathf.Clamp01(-((mousePosition.y - Camera.main.WorldToScreenPoint(transform.position).y)/Screen.height) + 0.5f));

			if(!grabbed){
				tempPandaPosition=transform.position;
				tempPandaPosition.y +=Input.mousePosition.y/Screen.height - 0.5f;
				if(Physics.Raycast(tempPandaPosition,new Vector3(0,0,1))) 
					grabbed = true;
			}
			else {
				rb.useGravity = false;
				rb.velocity = Vector3.zero;
				if(LPMDownTime < Time.time) {
					tempPandaPosition.y = (- mousePosition.y + mouseLockPosition.y)/2;
					tempPandaPosition.y /= Screen.height;
					tempPandaPosition.y += lockPosition.y;



					if(tempPandaPosition.y  - 0.7 >= 0)
						transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, tempPandaPosition.y, transform.position.z) , interpolation);
					else
						transform.position = lockPosition;
				}
				mouseDrag = oldMousePosition - mousePosition;
		
				moveVertical[0] = (mouseDrag.y/Screen.height);
				moveHorizontal[0] = (mouseDrag.x/Screen.height);
				for(int i = 14; i>0; --i) {
					moveVertical[i] = moveVertical[i-1];
					moveHorizontal[i] = moveHorizontal[i-1];
				}
			}	

			if(moveHorizontal[0] < speedXThreshold)
				
				transform.position = Vector3.Lerp(transform.position, new Vector3(other.transform.position.x, transform.position.y, transform.position.z) , interpolation);
		}

		if((Input.GetMouseButtonUp(0) && !Input.GetKey(KeyCode.Mouse1)) || (Input.GetMouseButtonUp(1) && !Input.GetKey(KeyCode.Mouse0)) || (Input.GetMouseButtonUp(1) && Input.GetMouseButtonUp(0))){
			anim.SetBool("ClimbingRight",false);
			grabbed = false;
			for(int i = 1; i<15; ++i){

				if(Mathf.Abs(moveVertical[0]) < Mathf.Abs(moveVertical[i]))
					moveVertical[0] = moveVertical[i];

				if(Mathf.Abs(moveHorizontal[0]) < Mathf.Abs(moveHorizontal[i]))
					moveHorizontal[0] = moveHorizontal[i];

				moveVertical[i] = 0;
				moveHorizontal[i] = 0;
			}

			float angle = Mathf.Atan2 (moveVertical[0], moveHorizontal[0]) * Mathf.Rad2Deg;
			if((angle < -70 && angle >-110) || (angle < 110 && angle >70))
				moveHorizontal[0] = 0;
			if(angle < -160 || angle >160 || (angle < 20 && angle >-20))
				moveVertical[0] = 0;

			moveVertical[0] *= speedY;
			moveHorizontal[0] *= speedX;

			if(moveVertical[0] > speedYMax)
				moveVertical[0] = speedYMax;
			if(moveVertical[0] < -speedYMax)
				moveVertical[0] = -speedYMax;
			if(moveHorizontal[0] > speedXMax)
				moveHorizontal[0] = speedXMax;
			if(moveHorizontal[0] < -speedXMax)
				moveHorizontal[0] = -speedXMax;
			rb.velocity = new Vector3(moveHorizontal[0], moveVertical[0], 0);	

			for(int i = 1; i<15; ++i) {
				moveVertical[i] = 0;
				moveHorizontal[i] = 0;
			}
		}
		oldMousePosition = mousePosition;
    }
}