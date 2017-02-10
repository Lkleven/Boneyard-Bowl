using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Menu : MonoBehaviour {
	public GameObject creditsPanel;

	// Use this for initialization
	void Start () {
		creditsPanel.SetActive (false);
	}

	public void ShowCredits(){
		creditsPanel.SetActive (true);
	}

	public void HideCredits(){
		creditsPanel.SetActive (false);
	}

	public void OpenURL(string url){
		Application.OpenURL (url);
	}
}
