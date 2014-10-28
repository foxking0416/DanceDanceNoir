using UnityEngine;
using System.Collections;

enum Action {None, Run, Jump, Slide, Left, Right, Up, Down, TurnLeft, TurnRight, Unlock};
enum Mode {Easy, Difficult};

public class Phase1 : MonoBehaviour {
	float musicBarLayerOffset;

	public GameObject notetoSpwan;
	public Material transMaterial;
	int timing;
	int noteSpwanDuration;
	int noteSpeedChangeTiming;
	int noteSpeedChangePeriod;

	int mode;
	int signal1, signal2;
	bool isToClear;
	string keySequence;
	string[] actionPatterns;

	int keyMiss;
	int maxKeyMiss;
	public GridMap gridMap;
	public AudioSource beatMissAudio1;
	public AudioSource beatMissAudio2;
	public AudioSource caseRemoveAudio;

	float noteStartX;
	public GUITexture beatBarBg;
	public Camera musicCamera;
	public HittingArea hittingArea;
	float screenWidth2World;
	int beatBarHeight;
	float actionBarWidth;

	public PlayerOne player1;
	public Character2Script player2;

	//visual effect
	public BeatFlashScript BeatFlashPlane;
	public PlayerMissBeatScript MissBeatFlashPlane;

	//audio effect
	public AudioSource beatAuido;
	
	ArrayList beatArray;
	float[] spectrum; 
	int beatId;
	
	//	int qSamples  = 1024;  // array size
	//	int eSamples = 44;
	//	int subbands = 32;
	//	int eId = -1;
	//	
	//	private float[] samples ; // audio samples
	//	private float[] spectrum; // audio spectrum
	//	private float fSample;
	//	private float[] E;
	//	private float[] Es;
	//	
	//	float last = 0.0f;
	
	public int numberOfCollectedEvidence;

	// Use this for initialization
	void Start () {
		musicBarLayerOffset = 150.0f;


		timing = noteSpwanDuration;
		noteSpwanDuration = 30;
		noteSpeedChangeTiming = noteSpeedChangePeriod;
		noteSpeedChangePeriod = 100;
		PlayerPrefs.SetFloat ("noteSpeed", 0.035f);


		beatBarHeight = (int)(Screen.height * 0.06);
		actionBarWidth = (float)(Screen.width * 0.3);
		beatBarBg.pixelInset = new Rect(-Screen.width/2,-beatBarHeight/2, Screen.width, beatBarHeight);

		Vector3 p = musicCamera.ViewportToWorldPoint (new Vector3 (1.0f, 0.5f, musicCamera.nearClipPlane));
		screenWidth2World = 2*(p.x-musicBarLayerOffset) * musicCamera.transform.position.y / (musicCamera.transform.position.y - p.y);
		PlayerPrefs.SetFloat ("ScreenWidth2World", screenWidth2World);
		float t = (float)(musicBarLayerOffset - (0.5-0.3*0.85)*screenWidth2World);
		PlayerPrefs.SetFloat ("HittingCenter", t);
		noteStartX = PlayerPrefs.GetFloat ("HittingCenter") + PlayerPrefs.GetFloat ("noteSpeed") * 250;//musicBarLayerOffset + screenWidth2World / 2;

		//define mode of the game 
		mode = (int)Mode.Easy;

		signal1 = 0;
		signal2 = 0;
		keySequence = "";

		isToClear = false;
		actionPatterns = new string[3];
		if (mode == (int)Mode.Easy) {
			actionPatterns[0] = " D";	actionPatterns[1] = " W";	actionPatterns[2] = " S";
		}
		else {
			actionPatterns[0] = " A D D";	actionPatterns[1] = " A W D";	actionPatterns[2] = " A S D";
		}

		keyMiss = 0;
		maxKeyMiss = 20;

		//player1 = GameObject.Find ("player_one(Clone)").GetComponent<PlayerOne> ();
		player2 = GameObject.FindGameObjectWithTag ("Player2").GetComponent<Character2Script>();

		//PlayerPrefs.SetInt ("Signal1", (int)Action.None);
		//PlayerPrefs.SetInt ("Signal2", (int)Action.None);

		//PlayerPrefs.SetInt ("HittingPeriod", 0);

		preGenerateMusicNote ();
		hittingArea = Instantiate (hittingArea, new Vector3 (PlayerPrefs.GetFloat ("HittingCenter"), 0.0f, 51.95f), Quaternion.identity) as HittingArea;

		//audio
//		qSamples  = 1024;  // array size
//		eSamples = 44;
//		subbands = 32;
//		eId = -1;
//
//		samples = new float[qSamples];
//		spectrum = new float[qSamples];
//		fSample = AudioSettings.outputSampleRate;
//		E = new float[eSamples];
//		Es = new float[subbands];

		numberOfCollectedEvidence = 0;
	}
	
	// Update is called once per frame
	void Update () {
		//AnalyzeSound();

		if (timing-- <= 0)
		{
			timing = noteSpwanDuration;
			generateNewNote(noteStartX);
		}

		if (noteSpeedChangeTiming-- <= 0)
		{
			noteSpeedChangeTiming = noteSpeedChangePeriod;
			noteSpeedIncrese();
		}

		if (isToClear)
		{
			keySequence = "";
			isToClear = false;
			//PlayerPrefs.SetString ("KeySequence", keySequence);
		}
		//PlayerPrefs.SetInt ("Signal1", (int)Action.None);
		//PlayerPrefs.SetInt ("Signal2", (int)Action.None);

		if(	numberOfCollectedEvidence >= 5)
			Application.LoadLevel("Phase2SceneV1");//Win

		playerKeyPressDectection ();
	}



	void playerKeyPressDectection()
	{
		MusicNote[] notes = FindObjectsOfType(typeof(MusicNote))as MusicNote[];
		MusicNote note1 = null;
		MusicNote note2 = null;
		
		foreach (MusicNote n in notes)
		{
			if (n.inBeatingArea())
			{
				if (n.tag == "P1P1Note")
					note1 = n;
				else if (n.tag == "P1P2Note")
					note2 = n;
			}
			if (n.tag == "TransNote" && n.inBeatingCenter()) {
				BeatFlashPlane.BeatFlash();
				beatAuido.audio.Play ();
				hittingArea.enlarge();
				Destroy(n.gameObject);

				GameObject[] stars = GameObject.FindGameObjectsWithTag("Enemy");
				foreach(GameObject s in stars){
					EnemyScript star = s.GetComponent<EnemyScript>();
					star.countBeat();
				}

				player2.DoNothing();
			}
		}
		
		//player one input dectection
		if ((mode == (int)Mode.Difficult && Input.GetKeyDown(KeyCode.A)) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.D))
		{
			if (note1 == null) {
				keyMiss++;
				MissBeatFlashPlane.Player1MissBeatFlash();
				beatMissAudio1.Play();
			}
			else
			{
				if (player1KeyPressDetection())
				{
					if (player1 == null)
						player1 = (PlayerOne)FindObjectOfType (typeof(PlayerOne));
					if (player1 != null)
						player1.trigger(signal1);
					Destroy(note1.gameObject);
				}
			}
		}
		//player two input detection
		if (Input.GetKeyDown(KeyCode.UpArrow) ||  Input.GetKeyDown(KeyCode.DownArrow)||  Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)|| Input.GetKeyDown(KeyCode.K)|| Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown(KeyCode.U))
		{
			if (note2 == null)
			{
				keyMiss++;
				MissBeatFlashPlane.Player2MissBeatFlash();
				beatMissAudio2.Play();
			}
			else
			{
				if (player2KeyPressDetection())
					Destroy(note2.gameObject);
			}
		}

		if(keyMiss >= maxKeyMiss)
		{
			keyMiss  = 0;
			gridMap.recreateCase();
			caseRemoveAudio.Play();
		}
	}

	public bool player1KeyPressDetection()
	{

		signal1 = (int)Action.None;
		
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
			if (player1 != null)
				player1.trigger(0);
			//isToClear = true;
			return false;
		}

		int patternLength = (mode == (int)Mode.Easy) ? 2 : 6;

		if (keySequence.Length >= patternLength)
		{
			if (player1 == null)
				player1 = (PlayerOne)FindObjectOfType (typeof(PlayerOne));

			if (keySequence == actionPatterns[0])
				signal1 = (int)Action.Run;
			else if (keySequence == actionPatterns[1])
				signal1 = (int)Action.Jump;
			else if (keySequence == actionPatterns[2])
				signal1 = (int)Action.Slide;
			else {
				isToClear = true;
				signal1 = (int)Action.None;
			}
			
			isToClear = true;
		}
		
		//PlayerPrefs.SetInt ("Signal1", signal1);
		//signal1 = (int)Action.None;
		//PlayerPrefs.SetString ("KeySequence", keySequence);

		return true;
	}

	public bool player2KeyPressDetection()
	{
//		if (!Input.anyKey)
//			return false;
		signal2 = (int)Action.None;

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
			signal2 = (int)Action.TurnLeft;
			player2.TurnLeft();
		}
		else if (Input.GetKeyDown(KeyCode.L)){
			signal2 = (int)Action.TurnRight;
			player2.TurnRight();
		}
		else if (Input.GetKeyDown(KeyCode.U)){
			signal2 = (int)Action.Unlock;
			player2.OpenCabinet();
		}
		else {
			signal2 = (int)Action.None;
			return false;
		}
		
		//PlayerPrefs.SetInt ("Signal2", signal2);
		return true;
	}

	void preGenerateMusicNote()
	{
		float endPos = PlayerPrefs.GetFloat ("HittingCenter");
		float curPos = noteStartX;
		while (curPos > endPos)
		{
			generateNewNote(curPos);
			curPos -= noteSpwanDuration * PlayerPrefs.GetFloat("noteSpeed");
		}
	}
	
	void generateNewNote(float posX)
	{
		GameObject p1 = Instantiate(notetoSpwan, new Vector3(posX, 0.0f, 51.95f), 
		                            Quaternion.Euler(new Vector3(0.0f, 90.0f, 90.0f))) as GameObject;
		p1.tag = "P1P1Note";
		
		GameObject p2 = Instantiate(notetoSpwan, new Vector3(posX, 0.0f, 51.95f), 
		                            Quaternion.Euler(new Vector3(0.0f, 90.0f, 90.0f))) as GameObject;
		p2.tag = "P1P2Note";


		GameObject p = Instantiate(notetoSpwan, new Vector3(posX, 0.0f, 20.0f), 
		                            Quaternion.Euler(new Vector3(0.0f, 90.0f, 90.0f))) as GameObject;
		p.renderer.material = transMaterial;
		p.tag = "TransNote";
	}
	
	void generateObstacle1()
	{
		float randValue = Random.Range(0, 2);
		GameObject gameObjCrate;
		if(randValue < 1){
			gameObjCrate = GameObject.FindGameObjectWithTag("HighCrateGen");
		}
		else{
			gameObjCrate = GameObject.FindGameObjectWithTag("LowCrateGen");
		}
		ObstacleGenerator obsGen = gameObjCrate.GetComponent<ObstacleGenerator>();
		obsGen.CreateCrate();
	}

	public void noteSpeedIncrese()
	{
		noteSpwanDuration -= 2;
		//noteSpwanDuration = (noteSpwanDuration < 18) ? 18 : noteSpwanDuration;
	}

	public void noteSpeedDecrease()
	{
		noteSpwanDuration += 2;
	}

	public void noteSpeedReset()
	{
		noteSpwanDuration = 30;
	}

	void OnGUI()
	{		
		//one player one part
		GUI.TextArea(new Rect((int)(0.7*Screen.width),10,(int)(Screen.width*0.25),20),"Obstacl Generator");
		GUI.backgroundColor = Color.red;
		if (keyMiss > 0)
			GUI.Button(new Rect((int)(0.7*Screen.width),10,(int)(Screen.width*0.25 * keyMiss / (float)maxKeyMiss), 20), "");

		//on music bar
		GUILayout.BeginArea(new Rect(0, (Screen.height-beatBarHeight)*0.32f, actionBarWidth, beatBarHeight));
		GUILayout.BeginHorizontal ();
		
		GUI.skin.button.fontSize = 15;
		GUI.backgroundColor = Color.white;
		GUI.Button (new Rect(0, 0, (float)(actionBarWidth*0.7), beatBarHeight), keySequence);

		GUI.backgroundColor = Color.green;
		//GUI.Button (new Rect((float)(actionBarWidth*0.7), 0, (float)(actionBarWidth * 0.3), beatBarHeight), "");
		
		GUILayout.EndHorizontal();
		GUILayout.EndArea();
	}

//	void AnalyzeSound(){
//		musicCamera.audio.GetOutputData(samples, 0); // fill array with samples
//		musicCamera.audio.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris); 
//
//		PlayerPrefs.SetFloat ("CurSpectrum", spectrum[5]);

//		for (int i=0; i < subbands; i++){ 
//			for (int j = i *32; j < (i+1) *32; j++) {
//				Es[i] += spectrum[j] * spectrum[j];
//			}
//			Es[i] /= (float)subbands;
//		}
//
//		//S1: instant sound energy
//		float instantE= 0;
//		for (int i=0; i < qSamples; i++){
//			instantE += samples[i]*samples[i]; // sum squared samples
//		}
//		
//		//S2: compute average energy
//		float aveE = 0.0f;
//		for (int i = 0; i < eSamples; i++)
//			aveE += E[i];
//		aveE /= (float)eSamples;
//		
//		//S3: variance of energies in E
//		float V = 0.0f;
//		for (int i = 0; i < eSamples; i++)
//			V += Mathf.Pow(E[i] - aveE, 2.0f);
//		V /= (float)eSamples;
//		
//		//S4:
//		double C = (-0.0025714 * V) + 1.5142857;
//		
//		//S5: repace the old E
//		eId = (++eId == eSamples) ? 0 : eId;
//		E [eId] = instantE;
//		
//		//S6: compare e > C*E
//		if (instantE > C * aveE)
//		{
//			Debug.Log (instantE.ToString() + " time:" + (audio.time - last).ToString());
//			last = audio.time;
//			generateNewNote(noteStartX);
//		}
//	}
}
