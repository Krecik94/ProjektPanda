using UnityEngine;
using System.Collections;

public class VineGenerator : MonoBehaviour {
	private float nextVineSpawn = 15.95f;
	private float nextSpawnTrigger = 10f;
	
	public GameObject Bamboo;
	
	void FixedUpdate () {
		if (transform.position.y > nextSpawnTrigger) { //sp
			nextSpawnTrigger += 1.773f;
			float number = Random.value;

			if (number<=0.75f)
				place1Vine();
			else if (number<=0.93)
				place2Vine();
			else
				place3Vine();
			nextVineSpawn += 1.773f;
		}
	}

	void place1Vine (){
		float number = Random.value;
		float position;

		if (number <= 0.333f)
			position = -1.5f;
		else if (number <= 0.666)
			position = 0f;
		else
			position = 1.5f;

		GameObject clone = (GameObject)Instantiate (Bamboo, new Vector3(position, nextVineSpawn, 0.275f), Bamboo.transform.rotation);
		clone.tag="Bamboo";
	}

	void place2Vine (){
		float number = Random.value;
		float position;
		
		if (number <= 0.333f)
			position = -1.5f;
		else if (number <= 0.666)
			position = 0f;
		else
			position = 1.5f;

		if (position != 0f) {
			GameObject clone = (GameObject)Instantiate (Bamboo, new Vector3 (0f, nextVineSpawn, 0.275f), Bamboo.transform.rotation);
			clone.tag = "Bamboo";
		}
		if (position != -1.5f) {
			GameObject clone = (GameObject)Instantiate (Bamboo, new Vector3 (-1.5f, nextVineSpawn, 0.275f), Bamboo.transform.rotation);
			clone.tag = "Bamboo";
		}
		if (position != 1.5f) {
			GameObject clone = (GameObject)Instantiate (Bamboo, new Vector3 (1.5f, nextVineSpawn, 0.275f), Bamboo.transform.rotation);
			clone.tag = "Bamboo";
		}
	}

	void place3Vine (){
		GameObject clone;

		clone = (GameObject)Instantiate (Bamboo, new Vector3 (1.5f, nextVineSpawn, 0.275f), Bamboo.transform.rotation);
		clone.tag = "Bamboo";
		clone = (GameObject)Instantiate (Bamboo, new Vector3 (-1.5f, nextVineSpawn, 0.275f), Bamboo.transform.rotation);
		clone.tag = "Bamboo";
		clone = (GameObject)Instantiate (Bamboo, new Vector3 (0f, nextVineSpawn, 0.275f), Bamboo.transform.rotation);
		clone.tag = "Bamboo";
	}
}
