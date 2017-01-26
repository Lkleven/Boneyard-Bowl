using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScoreMaster{
	private static int previousRoll;

	public static List<int> ScoreFrames (List<int> rolls){
		int rollCounter = 1;						//Number of rolls, will be MAXIMUM 21. I.E a strike will increment bowl in actionMaster by 2, but only 1 in rollcounter for scoring purposes
		int bowlCounter = 1;						//BowlCounter to keep track of frames
		List<int> frameList = new List<int> ();

		foreach (int roll in rolls) {
			Debug.Log (roll);
			//All frames filled
			if (frameList.Count == 10) {
				return frameList;
			}

			//Store previous Roll
			if (rollCounter > 1 ) {
				previousRoll = rolls [rollCounter - 2];
			}

			//Calculate score for strikes
			if (roll == 10){
				bowlCounter++; 							//Increments bowlcounter one additional time due to a strike skipping a bowl
				if (rolls.Count >= rollCounter + 2) {	//Only calculate score if there are two subsequent rolls to add to the score
					int score = roll + rolls [rollCounter] + rolls [rollCounter + 1];						
					frameList.Add (score);
				}
			}

			//Calculate End of frame score
			else if (bowlCounter % 2 == 0) {
				//Spare
				if (roll + previousRoll == 10){
					if(rolls.Count >= rollCounter + 1){			//Only calculate score if there are one subsequent roll to add to the score
						int score = roll + previousRoll + rolls [rollCounter];
						frameList.Add (score);
					}
				} else if (previousRoll == 10) {
					Debug.LogWarning ("Error, an end frame score should not have a previous roll of 10 possible");
				} else {
					int score = roll + previousRoll;
					frameList.Add (score);
				}
			}
			rollCounter++;
			bowlCounter++;
		}
		return frameList;
 	}

	//Calculates score so far
//	public int SumScore(List<int> frameList){
//		int scoreTotal = 0;
//		foreach (int frame in frameList) {
//			scoreTotal += frame;
//		}
//		return scoreTotal;
//	}

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
