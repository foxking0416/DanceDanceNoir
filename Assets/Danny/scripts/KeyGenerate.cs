using UnityEngine;
using System.Collections;

public class KeyGenerate : MonoBehaviour {

	public GameObject key;
	private GameObject gameObjPhase1;
	private GameObject gameObjGrid;
	private GlobalScript global;
	private Grid grid;

	// Use this for initialization
	void Start () {
		gameObjPhase1 = GameObject.Find("Phase1");
		global = gameObjPhase1.GetComponent<GlobalScript> ();
		gameObjGrid = GameObject.FindGameObjectWithTag ("Grid");
		grid = gameObjGrid.GetComponent< Grid > ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void createKey(int createKeyInLane){

		int keyColor = 0;

		bool iter = true;
		while(iter){
			iter = false;
			keyColor = Random.Range (31, 36);

			GameObject objKeyBlue = GameObject.FindGameObjectWithTag("BlueKey");
			if(objKeyBlue != null && keyColor == 31)
				iter = true;
			GameObject objKeyYellow = GameObject.FindGameObjectWithTag("YellowKey");
			if(objKeyYellow != null && keyColor == 32)
				iter = true;
			GameObject objKeyRed = GameObject.FindGameObjectWithTag("RedKey");
			if(objKeyRed != null && keyColor == 33)
				iter = true;
			GameObject objKeyGreen = GameObject.FindGameObjectWithTag("GreenKey");
			if(objKeyGreen != null && keyColor == 34)
				iter = true;
			GameObject objKeyOrange = GameObject.FindGameObjectWithTag("OrangeKey");
			if(objKeyOrange != null && keyColor == 35)
				iter = true;

			if(objKeyBlue != null && objKeyYellow != null && objKeyRed != null && objKeyGreen != null && objKeyOrange != null)
				return;
		}




		GameObject objKey = Instantiate( key, grid.computeCratePosition(grid.getWidth()-1, 2), transform.rotation ) as GameObject;
		switch (keyColor) {
		case 31:
			objKey.tag = "BlueKey";
			break;
		case 32:
			objKey.tag = "YellowKey";
			break;
		case 33:
			objKey.tag = "RedKey";
			break;
		case 34:
			objKey.tag = "GreenKey";
			break;
		case 35:
			objKey.tag = "OrangeKey";
			break;
		}

		int grid_width = grid.getWidth();


		grid.setObjectInGrid ( grid_width - 1, createKeyInLane, keyColor);


		Key keyScript = objKey.GetComponent<Key> ();
		keyScript.keyPositionDiscreteX = grid_width - 1;
		keyScript.keyPositionDiscreteY = createKeyInLane;
		keyScript.keyColor = keyColor;
		keyScript.SetColor (keyColor);
	}
}
