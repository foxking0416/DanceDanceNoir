using UnityEngine;
using System.Collections;

public class GameLoseScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI (){
		GUILayout.BeginArea(new Rect(100, 850, 140, 600));
		
		if (GUILayout.Button("New Game")) {
			Application.LoadLevel("Phase1SceneV4");
		}
		GUILayout.EndArea();
		
		
	}
}
