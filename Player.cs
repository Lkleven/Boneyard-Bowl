using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player{
	private string name;
	private bool finished = false;
	private int totalScore = 0;

	private List<int> bowls = new List <int> ();
	private Ball ball;
	private Color playerColor;


	public Player (string nameIn, Color colorFromPlayerSelector){
		name = nameIn;
		playerColor = colorFromPlayerSelector;
		ball = GameObject.FindObjectOfType<Ball> ();
		Debug.Log ("New Playerz");
	}

	public void PlayerFinishedGame(){
		finished = true;
		totalScore = ScoreMaster.CalculateTotalScore (bowls);
	}

	public void UpdateBallMaterial(){
		string number = name.Substring(6);
		int playerNumber = int.Parse(number);
		ball.SetPlayerMaterial(playerNumber);
	}
		



	//Getters
	public string GetName(){
		return name;
	}

	public List<int> GetBowls(){
		return bowls;
	}

	public Ball GetBall(){
		return ball;
	}

	public bool GetPlayerFinished(){
		return finished;
	}

	public int GetPlayerTotalScore(){
		return totalScore;
	}

	public Color GetPlayerColor(){
		return playerColor;
	}
		
		
		
}
