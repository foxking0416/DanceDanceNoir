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
		GUILayout.BeginArea(new Rect(480, 160, 140, 300));
		GUI.Button (new Rect(480, 160, 140, 200), "New Game");

		if (GUILayout.Button("New Game")) {
			Application.LoadLevel("Phase1SceneV4");
		}
		GUILayout.EndArea();
		GUILayout.BeginArea(new Rect(53, 410, 140, 200));
		if (GUILayout.Button("Introduction")) {
			Application.LoadLevel("IntroScene");
		}
		GUILayout.EndArea();

		GUILayout.BeginArea(new Rect(480, 668, 140, 200));
		if (GUILayout.Button("Exit")) {
			Application.Quit();
			Debug.Log ("Application.Quit() only works in build, not in editor");
		}
		GUILayout.EndArea();

	}
}
