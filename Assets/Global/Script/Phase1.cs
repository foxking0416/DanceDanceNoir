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

	public Character2Script player2;
	public PlayerOne player1;

	//audio
	int qSamples  = 1024;  // array size
	int eSamples = 44;
	int subbands = 32;
	int eId = -1;
	
	private float[] samples ; // audio samples
	private float[] spectrum; // audio spectrum
	private float fSample;
	private float[] E;
	private float[] Es;
	
	float last = 0.0f;

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
		keySequence = "";

		isToClear = false;
		actionPatterns = new string[3];
		actionPatterns[0] = " A D D";	actionPatterns[1] = " A W D";	actionPatterns[2] = " A S D";

		player2 = GameObject.Find ("Player").GetComponent<Character2Script>();
		//player1 = GameObject.Find ("player_one(Clone)").GetComponent<PlayerOne> ();

		PlayerPrefs.SetInt ("Signal1", (int)Action.None);
		PlayerPrefs.SetInt ("Signal2", (int)Action.None);

		PlayerPrefs.SetInt ("HittingPeriod", 0);

		//audio
		samples = new float[qSamples];
		spectrum = new float[qSamples];
		fSample = AudioSettings.outputSampleRate;
		E = new float[eSamples];
		Es = new float[subbands];
	}
	
	// Update is called once per frame
	void Update () {

		//AnalyzeSound();


		if (timing-- <= 0)
		{
			timing = noteSpwanDuration;
			generateNewNote();
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

	void generateNewNote()
	{
		GameObject p1 = Instantiate(notetoSpwan, new Vector3(musicBarLayerOffset + screenWidth2World/2, 0.0f, 50.0f), 
		                            Quaternion.Euler(new Vector3(0.0f, 90.0f, 90.0f))) as GameObject;
		p1.tag = "P1P1Note";
		
		GameObject p2 = Instantiate(notetoSpwan, new Vector3(musicBarLayerOffset + screenWidth2World/2, 0.0f, 50.0f), 
		                            Quaternion.Euler(new Vector3(0.0f, 90.0f, 90.0f))) as GameObject;
		p2.tag = "P1P2Note";
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
			if (player1 == null)
				player1 = (PlayerOne)FindObjectOfType (typeof(PlayerOne));

			if (keySequence == actionPatterns[0])
			{
				signal1 = (int)Action.Run;
				if (player1 != null)
					player1.sprint();
			}
			else if (keySequence == actionPatterns[1])
			{
				signal1 = (int)Action.Jump;
				if (player1 != null)
					player1.jump();
			}
			else if (keySequence == actionPatterns[2])
			{
				if (player1 != null)
					player1.slide();
				signal1 = (int)Action.Slide;
			}
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
			player2.MoveLeft();
		}
		else if (Input.GetKeyDown(KeyCode.RightArrow)){
			signal2 = (int)Action.Right;
			player2.MoveRight();
		}
		else if (Input.GetKeyDown(KeyCode.UpArrow)){
			signal2 = (int)Action.Up;
			player2.MoveUp();
		}
		else if (Input.GetKeyDown(KeyCode.DownArrow)){
			signal2 = (int)Action.Down;
			player2.MoveDown();
		}
		else if (Input.GetKeyDown(KeyCode.K)){
			signal2 = (int)Action.Key;
			player2.PickUpKey();
		}
		else if (Input.GetKeyDown(KeyCode.U)){
			signal2 = (int)Action.Unlock;
			player2.OpenCabinet();
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

	void AnalyzeSound(){
		audio.GetOutputData(samples, 0); // fill array with samples
		audio.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris); 

		for (int i=0; i < subbands; i++){ 
			for (int j = i *32; j < (i+1) *32; j++) {
				Es[i] += spectrum[j] * spectrum[j];
			}
			Es[i] /= (float)subbands;
		}

		//S1: instant sound energy
		float instantE= 0;
		for (int i=0; i < qSamples; i++){
			instantE += samples[i]*samples[i]; // sum squared samples
		}
		
		//S2: compute average energy
		float aveE = 0.0f;
		for (int i = 0; i < eSamples; i++)
			aveE += E[i];
		aveE /= (float)eSamples;
		
		//S3: variance of energies in E
		float V = 0.0f;
		for (int i = 0; i < eSamples; i++)
			V += Mathf.Pow(E[i] - aveE, 2.0f);
		V /= (float)eSamples;
		
		//S4:
		double C = (-0.0025714 * V) + 1.5142857;
		
		//S5: repace the old E
		eId = (++eId == eSamples) ? 0 : eId;
		E [eId] = instantE;
		
		//S6: compare e > C*E
		if (instantE > C * aveE)
		{
			Debug.Log (instantE.ToString() + " time:" + (audio.time - last).ToString());
			last = audio.time;
			generateNewNote();
		}

	}
}
