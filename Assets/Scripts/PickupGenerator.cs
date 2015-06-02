using UnityEngine;
using System.Collections;

public class PickupGenerator : MonoBehaviour {
	private float nextAppleSpawn = 15.95f;
	private float nextSpawnTrigger = 10f;
	
	public GameObject Apple;
	public float appleIntensity;

	void FixedUpdate () {
		if (transform.position.y > nextSpawnTrigger) {
			nextSpawnTrigger += appleIntensity;
			float number = Random.value;
			
			if (number>0.7f)
				generateApple();
			nextAppleSpawn += appleIntensity;
		}
	}

	void generateApple() {
		float number = Random.value;
		float position;
		
		if (number <= 0.333f)
			position = -1.5f;
		else if (number <= 0.666)
			position = 0f;
		else
			position = 1.5f;
		
		GameObject clone = (GameObject)Instantiate (Apple, new Vector3(position, nextAppleSpawn, 0f), Apple.transform.rotation);
		clone.tag="Apple";
	}
}
