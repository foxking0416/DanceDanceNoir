﻿using UnityEngine;
using System.Collections;

public class EnemyCar : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.transform.localPosition = new Vector3 (3, 0.0f, 0.0f);
		gameObject.transform.localRotation = Quaternion.Euler (new Vector3 (0, 270, 0));
		gameObject.transform.localScale = new Vector3 (0.3f, 0.3f, 0.3f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
