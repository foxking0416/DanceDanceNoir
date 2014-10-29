using UnityEngine;
using System.Collections;

public class GameInstructionScript3 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI (){
		GUILayout.BeginArea(new Rect(50, 500, 120, 200));
		if (GUILayout.Button("Previous Page")) {
			Application.LoadLevel("GameInstructionScene2");
		}
		GUILayout.EndArea();
		
		GUILayout.BeginArea(new Rect(300, 500, 100, 200));
		if (GUILayout.Button("Main Menu")) {
			Application.LoadLevel("GameStartScene");
		}
		GUILayout.EndArea();
	}
}
