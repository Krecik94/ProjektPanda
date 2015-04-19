using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour {

	public GameObject player;
	public float torque;


	private Vector3 offset;
	private Quaternion lastRotation;
	private float dest;
	private int k;


	// Use this for initialization
	void Start () {
		offset = transform.position - player.transform.position;
		lastRotation = player.transform.rotation;
		k = 0;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		dest = Input.GetAxis("Vertical");
		transform.position = player.transform.position + offset;
		if(dest > 0) k = 1;
		else if(dest < 0) k = -1;
			transform.RotateAround (player.transform.position,
									transform.up,
									k*Quaternion.Angle(player.transform.localRotation, lastRotation));
		offset = transform.position - player.transform.position;
		lastRotation = player.transform.rotation;
	}
}
