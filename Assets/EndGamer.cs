using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndGamer : MonoBehaviour {


	public GameObject Player;
	private Vector3 offset;
	private float minimumHeight;
	public Text endGame;
	
	// Use this for initialization
	void Start () {
		minimumHeight = transform.position.y - 4.0f;
		offset = transform.position - Player.transform.position;
		offset.x -= 0.3f;
		endGame.text = "";

	}
	
	// Update is called once per frame
	void Update () {
		
		if ((Player.transform.position + offset).y > minimumHeight) {
			transform.position = new Vector3 (transform.position.x,(Player.transform.position + offset).y,transform.position.z);
		} else {
			transform.position=new Vector3 (transform.position.x,minimumHeight,transform.position.z);
		}
		
		if (transform.position.y - 4.0f > minimumHeight) {
			minimumHeight = transform.position.y -4.0f ;
		}
		

		
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject.CompareTag ("Player")) {
			endGame.text = "You Lose";
			Time.timeScale = 0;
		}


		Destroy(other.gameObject);


	}
}
