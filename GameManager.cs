using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	private int players = 1;

	private Player[] player;

	private List<int> bowls = new List <int> ();
	private Ball ball;
	private PinSetter pinSetter;
	private ScoreDisplay scoreDisplay;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < players; i++){

		}

		ball = GameObject.FindObjectOfType<Ball> ();
		pinSetter = GameObject.FindObjectOfType<PinSetter> ();
		scoreDisplay = GameObject.FindObjectOfType<ScoreDisplay> ();
	}

	public void Bowl (int pinFall){
		try{
			bowls.Add (pinFall);
			ActionMaster.Action nextAction = ActionMaster.NextAction (bowls);
			pinSetter.PinMachine (nextAction);
			ball.Reset ();

		}catch{
			Debug.LogError ("Something went wrong in Bowl()");
		}
		try{
			scoreDisplay.FillRollCard (bowls);
			scoreDisplay.FillFrames (ScoreMaster.ScoreCumulative(bowls));
		}catch{
			Debug.LogWarning ("FillRollCard() error");
		}

	}

	public void SetPlayerCount(int playerCount){
		players = playerCount;
	}


}
