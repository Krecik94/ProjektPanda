using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.position = new Vector3 (transform.position.x-0.4f, transform.position.y, transform.position.z);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate () {

		transform.rotation = Quaternion.Euler (transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y+2.0f, transform.rotation.eulerAngles.z);
		
	}
}
