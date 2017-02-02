using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ScoreDisplay : MonoBehaviour {
	public Text[] rollTexts, frameTexts;

	// Use this for initialization
	void Start () {
		ClearScoreCard ();
	}

	public void FillRollCard(List<int> rolls){
		string scoresString = FormatRolls(rolls);
		for (int i = 0; i < scoresString.Length; i++) {
			rollTexts [i].text = scoresString [i].ToString();
		}
	}

	public void FillFrames(List<int> frames){
		for (int i = 0; i < frames.Count; i++) {
			frameTexts [i].text = frames [i].ToString ();
		}
	}

	public static string FormatRolls(List<int> rolls){
		string output = "";

		for (int i = 0; i < rolls.Count; i++){
			int box = output.Length + 1;										//Score box 1 to 21;

			 if (rolls [i] == 0) {												//ZERO
				output += "-";
			} else if ((box % 2 == 0 || box == 21)  && rolls [i - 1] + rolls [i] == 10) {		//SPARE
				output += "/";					
			} else if (box >= 19 && rolls[i] == 10){							//STRIKE in Frame 10
				output += "X";
			} else if (rolls[i] == 10){											//STRIKE in Frame 1-9
				output += "X "; /// remember the sapce
			} else {															//Normal 1-9 bowl
				output += rolls[i].ToString();								
			}
		}
			
		//Debug.Log (output);
		return output;
	}


	private void ClearScoreCard(){
		foreach (Text roll in rollTexts) {
			roll.text = "";
		}

		foreach (Text frame in frameTexts) {
			frame.text = "";
		}
	}
}
