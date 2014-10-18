using UnityEngine;
using System.Collections;

public class GlobalScript : MonoBehaviour {

	public int collectedEvidence;
	public int superEnergy;


	public int holdKeyStatus;
	public ArrayList openCabinetStatus;

	// Use this for initialization
	void Start () {
		openCabinetStatus = new ArrayList ();
		collectedEvidence = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool CompareColor(int color){
		for (int i = 0; i < openCabinetStatus.Count; ++i) {
			if(color == (int)openCabinetStatus[i])
				return true;
		}

		return false;
	}

	public void CollectEvidence(int color){
		collectedEvidence++;
		openCabinetStatus.Add (color);
		if(collectedEvidence == 5)
			Application.LoadLevel("Phase2Scene");//Win
	}

}
