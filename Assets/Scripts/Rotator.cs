using UnityEngine;
using System.Collections;

public class ROtator : MonoBehaviour {


	private Quaternion rotation1;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate () {

		transform.rotation= Quaternion.Euler (transform.rotation.eulerAngles.x,transform.rotation.eulerAngles.y+2,transform.rotation.eulerAngles.z);

	}

}
