﻿using UnityEngine;
using System.Collections;

public class MusicNote : MonoBehaviour {
	Vector3 orgPos;
	float speed;
	
	float hittingRange;
	public Material blinnOrange;

	public Phase1 phase1;
	
	// Use this for initialization
	void Start () {
		orgPos = gameObject.transform.position;
		speed = 0.03f;

		hittingRange = PlayerPrefs.GetFloat("ScreenWidth2World") * 0.3f * 0.25f;
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.position = new Vector3 (transform.position.x-speed, orgPos.y, orgPos.z);

		if (gameObject.transform.position.x < PlayerPrefs.GetFloat("HittingPos") + hittingRange)
		{
			if (gameObject.transform.position.x < PlayerPrefs.GetFloat("HittingPos"))
				Destroy (gameObject);

			if (gameObject.renderer.material != blinnOrange)
				gameObject.renderer.material = blinnOrange;

			phase1 = GameObject.Find("Phase1").GetComponent<Phase1>();

			if (gameObject.tag == "P1P1Note")
			{
				if (phase1.player1KeyPressDetection())
					Destroy (gameObject);
			}
			if (gameObject.tag == "P1P2Note")
			{
				if (phase1.player2KeyPressDetection())
					Destroy(gameObject);
			}
		}
	}



}
