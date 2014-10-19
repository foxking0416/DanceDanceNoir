﻿using UnityEngine;
using System.Collections;

public class MusicNote : MonoBehaviour {
	Vector3 orgPos;
	float speed;
	
	float hittingRange;
	public Material blinnOrange;

	public Phase1 phase1;
	public PlayerOne player1;
	//private BeatFlashScript beatFlash;
	
	// Use this for initialization
	void Start () {
		//beatFlash = (BeatFlashScript)FindObjectOfType (typeof(BeatFlashScript));
		orgPos = gameObject.transform.position;
		speed = PlayerPrefs.GetFloat("noteSpeed");

		hittingRange = PlayerPrefs.GetFloat("ScreenWidth2World") * 0.3f * 0.25f;
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.position = new Vector3 (transform.position.x-speed, orgPos.y, orgPos.z);

		if (gameObject.transform.position.x < PlayerPrefs.GetFloat("HittingPos") + hittingRange)
		{
			if (gameObject.transform.position.x < PlayerPrefs.GetFloat("HittingPos"))
			{
				if (player1 == null)
					player1 = (PlayerOne)FindObjectOfType (typeof(PlayerOne));
				if (gameObject.tag == "P1P1Note" && player1 != null)
					player1.trigger(0);
				Destroy (gameObject);
				//Debug.Log("0");
			}

			if (gameObject.renderer.material != blinnOrange)
				gameObject.renderer.material = blinnOrange;
		}
	}

	public bool inBeatingArea()
	{
		if (gameObject.transform.position.x < PlayerPrefs.GetFloat ("HittingPos") + hittingRange)
			return true;
		return false;
	}
}
