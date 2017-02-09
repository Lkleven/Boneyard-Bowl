using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	private int players = 1;
	private Player currentPlayer;
	//private Player[] player;
	private List<Player> playas = new List<Player> ();
	private PlayerCountGO pcGO;

	private PinSetter pinSetter;
	private ScoreDisplay scoreDisplay;
	private LevelManager levelManager;

	void Awake(){
		DontDestroyOnLoad (gameObject);
	}

	// Use this for initialization
	void Start () {
		pinSetter = GameObject.FindObjectOfType<PinSetter> ();
		scoreDisplay = GameObject.FindObjectOfType<ScoreDisplay> ();
		pcGO = GameObject.FindObjectOfType<PlayerCountGO> ();
		levelManager = GameObject.FindObjectOfType <LevelManager> ();

		try{
			players = pcGO.GetPlayerCount ();
		}catch{
			Debug.LogWarning ("WARNING: no PlayerCountGO found. Did you start from menu?");
		}
		CreatePlayers ();
	}

	// Creates players, initalizes them with name and player color.
	public void CreatePlayers(){
		for (int i = 0; i < players; i++){
			playas.Add (new Player ("Player"+(i+1), pcGO.GetPlayerColorsArray()[i]));
		}
		currentPlayer = playas [0];
	}

	// Handles the bowls (rolls) made by players
	public void Bowl (int pinFall){
		List<int> bowls = currentPlayer.GetBowls ();
		try{
			bowls.Add (pinFall);
			ActionMaster.Action nextAction = ActionMaster.NextAction (bowls);
			pinSetter.PinMachine (nextAction);
			currentPlayer.GetBall().Reset ();
			UpdateScore (bowls);

			if(nextAction == ActionMaster.Action.EndTurn){
				NextPlayer();
			}else if(nextAction == ActionMaster.Action.EndGame){
				currentPlayer.PlayerFinishedGame();
				NextPlayer();
			}
		}catch{
			Debug.LogError ("Something went wrong in Bowl()");
		}
	}

	/* Controls switching between players
	 * If a player is finished, the turn is passed on until all players are finished
	 */
	public void NextPlayer(){
		if (AllPlayersFinished ()) {
			GameOver ();
		}

		if (playas.Count - 1 > playas.IndexOf (currentPlayer)) { 
			currentPlayer = playas [playas.IndexOf (currentPlayer) + 1];
			if (currentPlayer.GetPlayerFinished()) {
				NextPlayer ();
			}
		} else {
			currentPlayer = playas [0];
		}
		currentPlayer.UpdateBallMaterial ();
		scoreDisplay.ClearScoreCard ();
		UpdateScore (currentPlayer.GetBowls ());
		 
	}

	void UpdateScore(List<int> bowls){
		try{
			scoreDisplay.FillRollCard (bowls);
			scoreDisplay.FillFrames (ScoreMaster.ScoreCumulative(bowls));
		}catch{
			Debug.LogWarning ("FillRollCard() error");
		}
	}

	//Returns true if all players are finished, false if not.
	bool AllPlayersFinished(){
		int finishedPlayerCount = 0;
		foreach (Player player in playas){
			if (player.GetPlayerFinished()) {
				finishedPlayerCount++;
				if (finishedPlayerCount == players) {
					return true;
				}
			}
		}
		return false;
	}

	//Fills pcGO's player array with the players. Loads the scoring scene.
	void GameOver(){
		pcGO.FillAllPlayersArray (playas);	//Copy Player references to persistent GO for scoring purposes
		levelManager.LoadNextLevel ();
	}

	public void SetPlayerCount(int playerCount){
		players = playerCount;
	}

	public List<Player> GetPlayers(){
		return playas;
	}


}
