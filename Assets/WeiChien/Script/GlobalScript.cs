using UnityEngine;
using System.Collections;

public class GlobalScript : MonoBehaviour {

	public int collectedEvidence;
	public int superEnergy;


	public int holdKeyStatus;
	public int openCabinetStatus;

	// Use this for initialization
	void Start () {
		collectedEvidence = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void CollectEvidence(){
		collectedEvidence++;

		if(collectedEvidence == 5)
			Application.LoadLevel("Phase2Scene");//Win
	}

}
