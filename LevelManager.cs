using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {


	public void Start(){

	}

	public void Update(){

	}


	public void ChangeLevel(string levelToLoad){
		SceneManager.LoadScene (levelToLoad);
	}

	public void LoadNextLevel(){
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
	}

	public void LoadPrevLevel(){
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex - 1);
	}

	public void Quit(){
		Application.Quit ();
	}
}
