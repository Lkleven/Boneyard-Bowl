using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;
using System.Linq;

/// <summary>
/// Player count GO, persistent through scenes.
/// Stores needed information between scenes.
/// Handles the final scoring display
/// </summary>

//TODO Change name of class when figuring out a more fitting one as the functions have changed
public class PlayerCountGO : MonoBehaviour {
	private int playerCount = 1;								//number of players in the game
	private bool gameOver = false;

	private List<Player> allPlayers = new List<Player> ();		// list of all players, to be sorted by final score when game is finished
	private ScoreDisplay[] scores;
	private Color[] playerColors;

	void Awake(){
		DontDestroyOnLoad (gameObject);
	}
		
	void Update(){
		if (SceneManager.GetActiveScene ().name.Equals ("GameOver") && !gameOver) {
			gameOver = true;

			scores = GameObject.FindObjectsOfType<ScoreDisplay> ();		// Get All ScoreDisplay objects
			scores = scores.OrderBy (go => go.name).ToArray ();			// Sort ScoreDisplay objects by name
			CalculateEndScoring ();
		}
	}

	public void SetPlayerCount (int players){
		playerCount = players;
	}

	public void SetPlayerColorsArray(Color[] colors){
		playerColors = colors;
	}
		

	public void FillAllPlayersArray(List<Player> players){
		allPlayers = players;
	}
		

	public int GetPlayerCount(){
		return playerCount;
	}

	public Color[] GetPlayerColorsArray(){
		return playerColors;
	}
		

	/* Calculates the final scoring
	*  Sorts the playerlist by players final score, in descending order
	*  Fills the scores array based on the sequence of the players
	*  Updates each score element to the players color for consistency
	*  Deactivate scores game objects that are unused. i.e. 3 players will deactive the 4th score sheet.
	*/
	void CalculateEndScoring(){
		List<Player> listByScore = new List<Player>();
		listByScore = allPlayers.OrderByDescending (s => s.GetPlayerTotalScore()).ToList ();

		for (int i = 0; i < scores.Length; i++){
			if (i < listByScore.Count) {
				scores [i].FillRollCard (listByScore [i].GetBowls ());
				scores [i].FillFrames (ScoreMaster.ScoreCumulative (listByScore [i].GetBowls ()));
				Image img = scores [i].GetComponent<Image> ();
				img.color = listByScore [i].GetPlayerColor ();

				Text text = scores[i].transform.Find ("Name").gameObject.GetComponent<Text> ();
				text.text = listByScore [i].GetName ();
				text.color = listByScore [i].GetPlayerColor ();

			} else {
				scores [i].gameObject.SetActive (false);
			}
		}
	}
}
