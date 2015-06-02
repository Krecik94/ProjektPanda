using UnityEngine;
using System.Collections;



public class Pause : MonoBehaviour {

	private bool pauseGame=false;
	private bool showUI=true;
	void Start(){
		(GameObject.Find ("GuiTexture").GetComponent<GUITexture> ()).enabled = false;
	}
	// Update is called once per frame
	void Update () {
	
		if(Input.GetKeyDown("p")){

		pauseGame = !pauseGame;

		if(pauseGame == true)
		{
			Time.timeScale=0;
			pauseGame=true;

			GameObject.Find("Player").GetComponent<PlayerController>().enabled = false;
			showUI=true;
		}

		if(pauseGame == false)
		{
			Time.timeScale=1;
			pauseGame=false;
	
			GameObject.Find("Player").GetComponent<PlayerController>().enabled = true;
			showUI=false;
		}
		if (showUI == true){

			(GameObject.Find("GuiTexture").GetComponent<GUITexture>()).enabled=true;
		}

		if (showUI == false){
				
			(GameObject.Find("GuiTexture").GetComponent<GUITexture>()).enabled=false;
		}

			if(Input.GetKeyDown("space")){
				Application.Quit();
			}
		}

	}
}
