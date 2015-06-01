﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private Rigidbody rb;
	private Animator anim;
	private float[] moveVertical;
	private float[] moveHorizontal;
	//private float mouseSpeedX;
	private float tempPandaYPosition;
	private Event mouseEvent;
	private Vector3 mousePosition;
	private Vector3 oldMousePosition;
	private Vector3 mouseDrag;
	private Vector3 lockPosition;
	private float LPMDownTime;
	private float LMPDownWait;

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
		tempPandaYPosition = 0f;
		moveHorizontal = new float[30];
		moveVertical = new float[30];
		LMPDownWait = 0.2f;
		pandaYSpeed.text = "Panda's Y velocity: " + rb.velocity.y.ToString();
		pandaXSpeed.text = "Panda's X velocity: " + rb.velocity.x.ToString();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void FixedUpdate () {
		if(rb.velocity.y > speedYMax)
			rb.velocity = new Vector3(rb.velocity.x, speedYMax, 0);
		if(rb.velocity.x > speedXMax)
			rb.velocity = new Vector3(speedXMax, rb.velocity.y, 0);
		if(rb.velocity.x < -speedXMax)
			rb.velocity = new Vector3(-speedXMax, rb.velocity.y, 0);
		SetMouseDelta();
		pandaYSpeed.text = "Panda's Y velocity: " + rb.velocity.y.ToString();
		pandaXSpeed.text = "Panda's X velocity: " + rb.velocity.x.ToString();
		anim.speed = Mathf.Abs (rb.velocity.y)/2;

		
	}

	void SetMouseDelta () {
		mouseDelta.text = (Screen.height/2).ToString() + " , " + Input.mousePosition.y.ToString();
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Bamboo")) {
			//rb.velocity = new Vector3(0, rb.velocity.y, 0);
			//transform.position = Vector3.Lerp(transform.position, new Vector3(other.transform.position.x, transform.position.y, transform.position.z) , interpolation);
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

    void PandaMove(Collider other) {
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
			mouseDrag = oldMousePosition - mousePosition;
			anim.Play("Idle_tmp",-1,Mathf.Abs((1-Mathf.Clamp((mousePosition.y/Screen.height),0,1))));
			moveVertical[0] = (mouseDrag.y/Screen.height);
			moveHorizontal[0] = (mouseDrag.x/Screen.height);
			for(int i = 29; i>0; --i) {
				moveVertical[i] = moveVertical[i-1];
				moveHorizontal[i] = moveHorizontal[i-1];
			}	

			if(moveHorizontal[0] < speedXThreshold)
				
				transform.position = Vector3.Lerp(transform.position, new Vector3(other.transform.position.x, transform.position.y, transform.position.z) , interpolation);
		}

		if(Input.GetMouseButtonUp(0)){

			for(int i = 1; i<30; ++i){

				if(Mathf.Abs(moveVertical[0]) < Mathf.Abs(moveVertical[i]))
					moveVertical[0] = moveVertical[i];

				if(Mathf.Abs(moveHorizontal[0]) < Mathf.Abs(moveHorizontal[i]))
					moveHorizontal[0] = moveHorizontal[i];
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
			if(moveHorizontal[0] > speedXMax)
				moveHorizontal[0] = speedXMax;
			if(moveHorizontal[0] < -speedXMax)
				moveHorizontal[0] = -speedXMax;
			rb.velocity = new Vector3(moveHorizontal[0], moveVertical[0], 0);	
		}
		oldMousePosition = mousePosition;
    }
}