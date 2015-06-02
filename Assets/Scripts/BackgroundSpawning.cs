using UnityEngine;
using System.Collections;

public class BackgroundSpawning : MonoBehaviour {
	private float nextTreeSpawn = 5.0f;

	public GameObject Background;

	void FixedUpdate () {
		if (transform.position.y > nextTreeSpawn) { //spawnowanie tekstury drzewa (warto by bylo jeszcze usuwac tekstury, ktore juz dawno przestal widziec)
			nextTreeSpawn += 10;
			Instantiate (Background, new Vector3(0.0f, nextTreeSpawn, 0.45f), Background.transform.rotation);
		}
	}
}
