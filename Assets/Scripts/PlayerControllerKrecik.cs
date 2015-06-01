using UnityEngine;
using System.Collections;

public class PlayerControllerKrecik : MonoBehaviour {
	
	private Vector2[] mousePositionHistory;
	private bool grabbed;
	private bool tryingToGrab;
	private Rigidbody myRigidBody;
	private float grabbingPoint;
	private Vector3 tempPosition;
	
	// Use this for initialization
	void Start () {
		
		mousePositionHistory = new Vector2[10];
		grabbed = false;
		tryingToGrab = false;
		myRigidBody = GetComponent<Rigidbody>();
		
		
		for(int i = 0; i<10; ++i) {
			mousePositionHistory[i]=new Vector2(-1,-1);
		}	
		
	}
	

	void FixedUpdate () {
		
		if (Input.GetMouseButtonDown (0)) 
		{
			tryingToGrab=true;
		}
		
		if (tryingToGrab && !grabbed) 
		{
			tempPosition=transform.position;
			tempPosition.y+=Input.mousePosition.y/Screen.height-0.5f;
			if(Physics.Raycast(tempPosition,new Vector3(0,0,1)))
			{
				grabbed=true;
				tryingToGrab=false;
				grabbingPoint=(Input.mousePosition.y/Screen.height)+transform.position.y-0.5f;
				for(int i = 0; i<10; ++i) {
					mousePositionHistory[i]=new Vector2 ( Input.mousePosition.x/Screen.width,Input.mousePosition.y/Screen.height);
				}

			}
		}
		
		
		
		
		if(Input.GetKey(KeyCode.Mouse0)&&grabbed)
		{
			myRigidBody.useGravity=false;
			for(int i = 9; i>0; --i) {
				mousePositionHistory[i]=mousePositionHistory[i-1];
			}	
			mousePositionHistory[0]=new Vector2( Input.mousePosition.x/Screen.width,Input.mousePosition.y/Screen.height);
			
			//Debug.Log(mousePositionHistory[1]);
			tempPosition = transform.position;

			if(mousePositionHistory[1].y!=-1 && mousePositionHistory[0].y !=-1)
			tempPosition.y += mousePositionHistory[1].y - mousePositionHistory[0].y;
			Debug.Log(mousePositionHistory[1].y - mousePositionHistory[0].y);
			//if(mousePositionHistory[1].y - mousePositionHistory[0].y < 0.001)
			//tempPosition.y-=0.001f;
			transform.position=tempPosition;
			
			Debug.Log(grabbingPoint);
			if(transform.position.y < grabbingPoint -0.5f)
			{
				tempPosition=transform.position;
				tempPosition.y=grabbingPoint-0.5f;
				transform.position=tempPosition;
			}
			
			if(transform.position.y > grabbingPoint +0.5f)
			{
				tempPosition=transform.position;
				tempPosition.y=grabbingPoint+0.5f;
				transform.position=tempPosition;
			}
			
		}
		
		
		if (Input.GetMouseButtonUp (0) && grabbed) {
			grabbed=false;
			tryingToGrab=false;

			myRigidBody.useGravity=true;

			for(int i = 9; i>-1; --i) {
				if(mousePositionHistory[i].y != -1)
				{
					myRigidBody.velocity=new Vector3(0.0f,(mousePositionHistory[i].y-mousePositionHistory[0].y)*5.0f,0.0f);

				break;
				}
			}
			for(int i = 0; i<10; ++i) {
				mousePositionHistory[i]=new Vector2(-1,-1);
			}
		}
		
		
		
		
	}
}
