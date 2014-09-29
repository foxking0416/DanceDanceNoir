using UnityEngine;
using System.Collections;

enum Action {None, Run, Jump, Slide, Left, Right, Up, Down, Key, Unlock};

public class Phase1 : MonoBehaviour {
	float musicBarLayerOffset;

	public GameObject notetoSpwan;
	int timing;
	int noteSpwanDuration;

	int signal1, signal2;
	bool isToClear;
	string keySequence;
	string[] actionPatterns;

	public GUITexture beatBarBg;
	public Camera musicCamera;
	float screenWidth2World;
	int beatBarHeight;
	float actionBarWidth;

	// Use this for initialization
	void Start () {
		musicBarLayerOffset = 150.0f;

		timing = 0;
		noteSpwanDuration = 25;

		beatBarHeight = (int)(Screen.height * 0.06);
		actionBarWidth = (float)(Screen.width * 0.3);
		beatBarBg.pixelInset = new Rect(-Screen.width/2,-beatBarHeight/2, Screen.width, beatBarHeight);

		Vector3 p = musicCamera.ViewportToWorldPoint (new Vector3 (1.0f, 0.5f, musicCamera.nearClipPlane));
		screenWidth2World = 2*(p.x-musicBarLayerOffset) * musicCamera.transform.position.y / (musicCamera.transform.position.y - p.y);
		PlayerPrefs.SetFloat ("ScreenWidth2World", screenWidth2World);
		float t = (float)(musicBarLayerOffset - (0.5 - 0.3 * 0.75) * screenWidth2World);
		PlayerPrefs.SetFloat ("HittingPos", t);

		signal1 = 0;
		signal2 = 0;

		isToClear = false;
		actionPatterns = new string[3];
		actionPatterns[0] = " A D D";	actionPatterns[1] = " A W D";	actionPatterns[2] = " A S D";
		

		PlayerPrefs.SetInt ("Signal1", (int)Action.None);
		PlayerPrefs.SetInt ("Signal2", (int)Action.None);

		PlayerPrefs.SetInt ("HittingPeriod", 0);
	}
	
	// Update is called once per frame
	void Update () {

		if (timing-- <= 0)
		{
			timing = noteSpwanDuration;
			GameObject p1 = Instantiate(notetoSpwan, new Vector3(musicBarLayerOffset + screenWidth2World/2, 0.0f, 50.0f), 
			            	Quaternion.Euler(new Vector3(0.0f, 90.0f, 90.0f))) as GameObject;
			p1.tag = "P1P1Note";

			GameObject p2 = Instantiate(notetoSpwan, new Vector3(musicBarLayerOffset + screenWidth2World/2, 0.0f, 50.0f), 
			                            Quaternion.Euler(new Vector3(0.0f, 90.0f, 90.0f))) as GameObject;
			p2.tag = "P1P2Note";
		}
		if (isToClear)
		{
			keySequence = "";
			isToClear = false;
			PlayerPrefs.SetString ("KeySequence", keySequence);
		}
		PlayerPrefs.SetInt ("Signal1", (int)Action.None);
		PlayerPrefs.SetInt ("Signal2", (int)Action.None);
	}

	public bool player1KeyPressDetection()
	{
		if (!Input.anyKey)
			return false;

		if (Input.GetKeyDown (KeyCode.A)) {
			if (keySequence.Length != 0)
				isToClear = true;
			keySequence += " A";
		}
		else if (Input.GetKeyDown(KeyCode.S)){
			if (keySequence.Length == 0)
				isToClear = true;
			keySequence += " S";
		}
		else if (Input.GetKeyDown(KeyCode.D)){
			if (keySequence.Length == 0)
				isToClear = true;
			keySequence += " D";
		}
		else if (Input.GetKeyDown(KeyCode.W)){
			if (keySequence.Length == 0)
				isToClear = true;
			keySequence += " W";
		}
		else{
			isToClear = true;
		}
		
		if (keySequence.Length >= 6)
		{
			if (keySequence == actionPatterns[0])
				signal1 = (int)Action.Run;
			else if (keySequence == actionPatterns[1])
				signal1 = (int)Action.Jump;
			else if (keySequence == actionPatterns[2])
				signal1 = (int)Action.Slide;
			else
				isToClear = true;
			
			isToClear = true;
		}
		
		PlayerPrefs.SetInt ("Signal1", signal1);
		signal1 = (int)Action.None;
		PlayerPrefs.SetString ("KeySequence", keySequence);

		return true;
	}

	public bool player2KeyPressDetection()
	{
		if (!Input.anyKey)
			return false;

		if (Input.GetKeyDown(KeyCode.LeftArrow)){
			signal2 = (int)Action.Left;
		}
		else if (Input.GetKeyDown(KeyCode.RightArrow)){
			signal2 = (int)Action.Right;
		}
		else if (Input.GetKeyDown(KeyCode.UpArrow)){
			signal2 = (int)Action.Up;
		}
		else if (Input.GetKeyDown(KeyCode.DownArrow)){
			signal2 = (int)Action.Down;
		}
		else if (Input.GetKeyDown(KeyCode.K)){
			signal2 = (int)Action.Key;
		}
		else if (Input.GetKeyDown(KeyCode.U)){
			signal2 = (int)Action.Unlock;
		}
		else {
			signal2 = (int)Action.None;
		}

		PlayerPrefs.SetInt ("Signal2", signal2);
		return true;
	}

	void OnGUI()
	{
		GUILayout.BeginArea(new Rect(0, (Screen.height-beatBarHeight)/2, actionBarWidth, beatBarHeight));
		GUILayout.BeginHorizontal ();
		
		GUI.skin.button.fontSize = 15;
		GUI.backgroundColor = Color.white;
		GUI.Button (new Rect(0, 0, (float)(actionBarWidth*0.7), beatBarHeight), PlayerPrefs.GetString("KeySequence"));
		
		int s2 = PlayerPrefs.GetInt("Signal2");
		if (PlayerPrefs.GetInt("Signal1") != (int)Action.None || PlayerPrefs.GetInt("Signal2") != (int)Action.None)
			GUI.backgroundColor = Color.red;
		else
			GUI.backgroundColor = Color.green;
		GUI.Button (new Rect((float)(actionBarWidth*0.7), 0, (float)(actionBarWidth * 0.3), beatBarHeight), PlayerPrefs.GetInt("Signal2").ToString());
		
		GUILayout.EndHorizontal();
		GUILayout.EndArea();
	}
}
