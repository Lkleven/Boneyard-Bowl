using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SoundControls : MonoBehaviour {
	public Image soundOn;
	public Image soundOff;

	private PlayerCountGO pcGO;

	// Use this for initialization
	void Start () {
		pcGO = GameObject.FindObjectOfType<PlayerCountGO> ();
		soundOff.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ToggleMusic(){
		if (soundOff.gameObject.activeSelf) {
			soundOn.gameObject.SetActive (true);
			soundOff.gameObject.SetActive (false);
			pcGO.ResumeMusic ();
		} else {
			soundOff.gameObject.SetActive (true);
			soundOn.gameObject.SetActive (false);
			pcGO.PauseMusic ();
		}
	}
}
