using UnityEngine;
using System.Collections;

public class BeatFlashScript : MonoBehaviour {

	public Material beatFlashMaterial1;
	public Material beatFlashMaterial2;
	private bool beatFlash = true;

	private float flashPeriod;
	private float flashTime;

	private bool Flag;

	// Use this for initialization
	void Start () {
		flashPeriod = 0.1f;
		flashTime = 0;
		Flag = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.Z))
			BeatFlash ();


		if (Flag == true) {
			gameObject.renderer.material = beatFlashMaterial2;
			flashTime += Time.deltaTime;
			if(flashTime > flashPeriod)	{
				gameObject.renderer.material = beatFlashMaterial1;
				flashTime = 0;
				Flag = false;
			}	
		}
	}

	public void BeatFlash(){
		Flag = true;
	}
}
