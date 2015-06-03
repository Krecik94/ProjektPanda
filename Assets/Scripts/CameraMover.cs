using UnityEngine;
using System.Collections;

public class CameraMover : MonoBehaviour {

	public GameObject Player;
	private Vector3 offset;
	private float minimumHeight;

	// Use this for initialization
	void Start () {
		minimumHeight = transform.position.y - 4.0f;
		offset = transform.position - Player.transform.position;
		offset.x -= 0.3f;
	}
	
	// Update is called once per frame
	void Update () {

		if ((Player.transform.position + offset).y > minimumHeight) {
			transform.position = Player.transform.position + offset;
		} else {
			transform.position=new Vector3 (transform.position.x,minimumHeight,transform.position.z);
		}

		if (transform.position.y - 4.0f > minimumHeight) {
			minimumHeight = transform.position.y - 4.0f ;
		}

		if (transform.position.x  < -2.0f) {
			transform.position=new Vector3 (-2.0f,transform.position.y,transform.position.z);
		}

		if (transform.position.x  > 2.0f) {
			transform.position=new Vector3 (2.0f,transform.position.y,transform.position.z);
		}
	
	}
}
