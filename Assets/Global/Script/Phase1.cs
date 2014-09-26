using UnityEngine;
using System.Collections;

enum Action {Run, Jump, Slide, Left, Right, Up, Down, Key, Unlock};

public class Phase1 : MonoBehaviour {
	public GameObject yNotetoSpwan;
	public int timing;
	public int yNoteSpwanDuration;
	
	public GUITexture beatBarBg;
	public int beatBarHeight;
	public float actionBarWidth;
	// Use this for initialization
	void Start () {
		timing = 0;
		yNoteSpwanDuration = 20;
		
		beatBarHeight = (int)(Screen.height * 0.07);
		actionBarWidth = (float)(Screen.width * 0.3);
		beatBarBg.pixelInset = new Rect(-Screen.width/2,-beatBarHeight/2, Screen.width, beatBarHeight);
		Vector3 p = Camera.main.ViewportToWorldPoint(new Vector3((float)(0.3 * 0.9), 0.5f, Camera.main.nearClipPlane));
		PlayerPrefs.SetFloat("HittingPos", p.x * Camera.main.transform.position.z /(Camera.main.transform.position.z - p.z));
	}
	
	// Update is called once per frame
	void Update () {
		if (timing-- <= 0)
		{
			timing = yNoteSpwanDuration;
			Instantiate(yNotetoSpwan, new Vector3(15.0f, 0.0f, 0.0f), 
			            Quaternion.AngleAxis(90, new Vector3(0.0f, 1.0f, 0.0f)));
		}
	}
	void OnGUI()
	{
		GUILayout.BeginArea(new Rect(0, (Screen.height-beatBarHeight)/2, actionBarWidth, beatBarHeight));
		GUILayout.BeginHorizontal ();
		
		GUI.skin.button.fontSize = 15;
		GUI.backgroundColor = Color.green;
		GUI.Button (new Rect(0, 0, (float)(actionBarWidth*0.8), beatBarHeight), "");
		
		GUI.backgroundColor = Color.red;
		GUI.Button (new Rect((float)(actionBarWidth*0.8), 0, (float)(actionBarWidth * 0.2), beatBarHeight),"");
		
		GUILayout.EndHorizontal();
		GUILayout.EndArea();
	}
}
