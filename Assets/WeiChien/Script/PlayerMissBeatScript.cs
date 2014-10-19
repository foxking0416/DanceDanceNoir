using UnityEngine;
using System.Collections;

public class PlayerMissBeatScript : MonoBehaviour {


	private bool beatFlash = true;
	
	private float flashPeriod;
	private float flashTime1;
	private float flashTime2;
	
	private bool flag1;
	private bool flag2;
	private GameObject player1MissBeatCamera;
	private GameObject player2MissBeatCamera;
	// Use this for initialization
	void Start () {
		flashPeriod = 0.1f;
		flashTime1 = 0;
		flashTime2 = 0;
		flag1 = false;
		flag2 = false;

		player1MissBeatCamera = GameObject.Find ("Player1BeatMissCamera");
		player1MissBeatCamera.camera.depth =0;
		player2MissBeatCamera = GameObject.Find ("Player2BeatMissCamera");
		player2MissBeatCamera.camera.depth =0;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown(KeyCode.X))
			Player1MissBeatFlash ();
		if (Input.GetKeyDown(KeyCode.C))
			Player2MissBeatFlash ();
		
		if (flag1 == true) {
			player1MissBeatCamera.camera.depth =2;
			flashTime1 += Time.deltaTime;
			if(flashTime1 > flashPeriod)	{
				player1MissBeatCamera.camera.depth =0;
				flashTime1 = 0;
				flag1 = false;
			}	
		}

		if (flag2 == true) {
			player2MissBeatCamera.camera.depth =2;
			flashTime2 += Time.deltaTime;
			if(flashTime2 > flashPeriod)	{
				player2MissBeatCamera.camera.depth =0;
				flashTime2 = 0;
				flag2 = false;
			}	
		}
	}
	
	public void Player1MissBeatFlash(){
		flag1 = true;
	}

	public void Player2MissBeatFlash(){
		flag2 = true;
	}
}
