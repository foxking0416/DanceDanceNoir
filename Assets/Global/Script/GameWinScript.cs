using UnityEngine;
using System.Collections;

public class GameWinScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI (){
		GUILayout.BeginArea(new Rect(10, 550, 140, 600));
		
		if (GUILayout.Button("Main Menu")) {
			Application.LoadLevel("GameStartScene");
		}
		GUILayout.EndArea();

		
	}
}
