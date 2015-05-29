using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RArmMovement : MonoBehaviour {


	public GameObject player;
	private Vector3 offset;
	private Vector3 difference;
	public Rigidbody myRigidBody;
	private Vector3 currentPosition;
	private Vector3 mouseForce;
	public Text debugText;
	private Vector3 tempOffset;
	// Use this for initialization
	void Start () {
		offset = transform.position - player.transform.position;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate()
	{


		if (Input.GetKey (KeyCode.Mouse0)&&Input.mousePosition.x>=Screen.width/2) {
			mouseForce.y =  Input.mousePosition.y - (Screen.height/2);
			mouseForce.x=-0.2f;
			mouseForce.z=0;
			mouseForce.y/=Screen.height;
			currentPosition = transform.position - player.transform.position;
			tempOffset=offset+(mouseForce*3f);
			difference=tempOffset-currentPosition ;

			myRigidBody.AddForce(difference*1110);
			//myRigidBody.AddForce(mouseForce*1000);
			debugText.text="Pressed" + mouseForce.ToString();

		} else {
			currentPosition = transform.position - player.transform.position;
			difference=offset-currentPosition ;
			myRigidBody.AddForce(difference*1110);
			debugText.text="Not pressed" + difference.ToString();

		}



	}

}
