using UnityEngine;
using System.Collections;

public class TitleScript : MonoBehaviour {

	public AudioClip titleAudio;

	// Use this for initialization
	void Start () {
		//AudioSource.PlayClipAtPoint (titleAudio, gameObject.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void OnGUI (){
		GUILayout.BeginArea(new Rect(300, 160, 140, 300));
		//GUI.Button (new Rect(480, 160, 140, 200), "New Game");

		if (GUILayout.Button("New Game")) {
			Application.LoadLevel("Phase1SceneV4");
		}
		GUILayout.EndArea();
		GUILayout.BeginArea(new Rect(53, 410, 140, 200));
		if (GUILayout.Button("Introduction")) {
			Application.LoadLevel("GameInstructionScene");
		}
		GUILayout.EndArea();

		GUILayout.BeginArea(new Rect(300, 500, 140, 200));
		if (GUILayout.Button("Exit")) {
			Application.Quit();
			Debug.Log ("Application.Quit() only works in build, not in editor");
		}
		GUILayout.EndArea();
	}


}
