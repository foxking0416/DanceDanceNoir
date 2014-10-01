using UnityEngine;
using System.Collections;

public class TitleScript : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnGUI (){
		GUILayout.BeginArea(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 100, 100, 200));
		// Load the main scene
		// The scene needs to be added into build setting to be loaded!
		if (GUILayout.Button("New Game")) {
			Application.LoadLevel("GameplayScene");
		}

		if (GUILayout.Button("Introduction")) {
			Application.LoadLevel("IntroScene");
		}

		if (GUILayout.Button("High score"))	{
			Application.LoadLevel("HighScoreScene");
		}

		if (GUILayout.Button("Exit")) {
			Application.Quit();
			Debug.Log ("Application.Quit() only works in build, not in editor");
		}
		GUILayout.EndArea();
	}
}
