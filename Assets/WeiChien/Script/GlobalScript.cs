using UnityEngine;
using System.Collections;

public class GlobalScript : MonoBehaviour {

	public int collectedEvidence;
	public int superEnergy;


	public int holdKeyStatus;
	public ArrayList openCabinetStatus;
	private GameObject gameObjTextStatus;

	// Use this for initialization
	void Start () {
		gameObjTextStatus = GameObject.FindGameObjectWithTag("TextStatus");

		openCabinetStatus = new ArrayList ();
		collectedEvidence = 0;
	}
	
	// Update is called once per frame
	void Update () {
		/*Color orange = new Vector4 (1, 0.8f, 0, 1);
		switch (holdKeyStatus) {
		case 31:
			gameObjTextStatus.guiText.text = "You are holding the Blue Key!";
			gameObjTextStatus.guiText.color = Color.blue;
			break;
		case 32:
			gameObjTextStatus.guiText.text = "You are holding the Yellow Key!";
			gameObjTextStatus.guiText.color = Color.yellow;
			break;
		case 33:
			gameObjTextStatus.guiText.text = "You are holding the Red Key!";
			gameObjTextStatus.guiText.color = Color.red;
			break;
		case 34:
			gameObjTextStatus.guiText.text = "You are holding the Green Key!";
			gameObjTextStatus.guiText.color = Color.green;
			break;
		case 35:
			gameObjTextStatus.guiText.text = "You are holding the Orange Key!";
			gameObjTextStatus.guiText.color = orange;
			break;
		default:
			gameObjTextStatus.guiText.text = "You do not have any key on hand!";
			gameObjTextStatus.guiText.color = Color.black;
			break;
		}*/
		//gameObjTextStatus.guiText.

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
