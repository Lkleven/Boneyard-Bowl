using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Diagnostics;
using Debug = UnityEngine.Debug;
using System.Runtime.CompilerServices;

public static class ScoreMaster{
	private static int previousRoll;

	/// <summary>
	/// FIX DIS' CRAP
	/// </summary>
	/// <returns>The frames.</returns>
	/// <param name="rolls">Rolls.</param>

	//Return a list of individual frame scores
	public static List<int> ScoreFrames (List<int> rolls){
		List<int> frameList = new List<int> ();


//		************************************
//		** ScoreFrames Test from lecture. **
//		************************************
		for (int i = 1; i < rolls.Count; i += 2) {
			if (frameList.Count == 10) {
				break;
			}						//prevents 11th frame

			if (rolls [i - 1] + rolls [i] < 10) { 					//normal "open" frame
				frameList.Add (rolls [i - 1] + rolls [i]);
			}

			if (rolls.Count - i <= 1) {
				break;
			}						//insufficient look-ahead

			if (rolls [i - 1] == 10) {								//STRIKE
				i--;												//Strike frame has just one roll
				frameList.Add (10 + rolls [i + 1] + rolls [i + 2]);
			} else if (rolls [i - 1] + rolls [i] == 10) {			//Calculate SPARE bonus
				frameList.Add (10 + rolls [i + 1]);
			}
		}
		return frameList;
	}

//		************************************************************************************************
//		************************************************************************************************

	//Calculates score so far
//	public int SumScore(List<int> frameList){
//		int scoreTotal = 0;
//		foreach (int frame in frameList) {
//			scoreTotal += frame;
//		}
//		return scoreTotal;
//	}


//	//Store previous Roll
//	if (rollCounter > 1 ) {
//		previousRoll = rolls [rollCounter - 2];
//	}
//
//
//	//Calculate score for strikes
//	if (bowlCounter % 2 != 0 && roll == 10){
//		Debug.Log ("Strike");
//		bowlCounter++; 							//Increments bowlcounter one additional time due to a strike skipping a bowl
//		if (rolls.Count >= rollCounter + 2) {	//Only calculate score if there are two subsequent rolls to add to the score
//			//Debug.Log("Calculating Strike");
//			int score = roll + rolls [rollCounter] + rolls [rollCounter + 1];						
//			frameList.Add (score);
//		}
//	}
//
//	//Calculate End of frame score
//	else if (bowlCounter % 2 == 0) {
//		//Debug.Log ("EndOfFrame");
//		//Spare
//		if (roll + previousRoll == 10){
//			if(rolls.Count >= rollCounter + 1){			//Only calculate score if there are one subsequent roll to add to the score
//				int score = roll + previousRoll + rolls [rollCounter];
//				frameList.Add (score);
//			}
//		} else if (previousRoll == 10) {
//			Debug.LogWarning ("Error, an end frame score should not have a previous roll of 10 possible. Roll Number: " + roll);
//		} else {
//			int score = roll + previousRoll;
//			frameList.Add (score);
//		}
//	}
//	//All frames filled, prevents 11th frame
//	if (frameList.Count == 10) {
//		Debug.Log ("GameOver");
//		return frameList;
//		//Figure out how to End/Reset Game
//	}
//
//	rollCounter++;
//	bowlCounter++;
//}
//return frameList;
//}
//
	public static List<int> ScoreCumulative(List<int> rolls){
		List<int> cumulativeScores = new List<int> ();
		int runningTotal = 0;
	
		foreach (int frameScore in ScoreFrames(rolls)){
			runningTotal += frameScore;
			cumulativeScores.Add (runningTotal);
		}
		return cumulativeScores;
	}
}
