using UnityEngine;
using System.Collections;

public class MusicNote : MonoBehaviour {
	Vector3 orgPos;
	Vector3 orgScale;
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
		orgScale = gameObject.transform.localScale;
		speed = PlayerPrefs.GetFloat("noteSpeed");

		hittingRange = PlayerPrefs.GetFloat("ScreenWidth2World") * 0.3f * 0.23f;
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.position = new Vector3 (transform.position.x-speed, orgPos.y, orgPos.z);
		//scaleToMusic ();

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
			{
				gameObject.renderer.material = blinnOrange;
				gameObject.transform.localScale = new Vector3(4.0f, 4.0f, 4.0f);
			}
		}
	}

	public bool inBeatingArea()
	{
		if (gameObject.transform.position.x < PlayerPrefs.GetFloat ("HittingPos") + hittingRange)
			return true;
		return false;
	}

	public bool inBeatingCenter()
	{
		float begin = PlayerPrefs.GetFloat ("HittingPos") + hittingRange;
		if ( gameObject.transform.position.x < begin && Mathf.Abs( gameObject.transform.position.x - begin)<0.1)
			return true;
		return false;
	}

	void scaleToMusic()
	{
		float scale = PlayerPrefs.GetFloat("CurSpectrum") * 50; //* scaleSize;
		if (scale < 0.6f * orgScale.x)
			scale = 0.6f * orgScale.x;
		else if (scale > orgScale.x * 1.2f)
			scale = orgScale.x;

		gameObject.transform.localScale = new Vector3(scale, scale, scale);
	}
}
