using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionMaster{
	public enum Action{Tidy, Reset, EndTurn, EndGame};
	private int[] bowls = new int[21]; // A game of bowling got up to 21 bowls 2x9 + potential 3.
	private int bowl = 1;

	public static Action NextAction (List<int> pinFalls){
		ActionMaster am = new ActionMaster ();
		Action currentAction = new Action ();

		foreach (int pinFall in pinFalls) {
			currentAction = am.Bowl (pinFall);
		}
		return currentAction;

	}

	public Action Bowl (int pins){ //TODO make private
		if (pins < 0 || pins > 10) {throw new UnityException ("Invalid number of pins");}
		bowls [bowl - 1] = pins;

		//Handles last frame special cases
		if (bowl >= 19 && bowl < 21 && Bowl21Awarded ()) {
			if (bowl == 20 && bowls[bowl-2] == 10 && pins < 10) {		//If roll 19 is a strike and roll 20 fails to knock down all pins. Return Tidy;
				bowl++;
				return Action.Tidy;
			}
			bowl++;
			return Action.Reset;
		}

		//Handles last balls
		if(bowl == 21 || (bowl == 20 && !Bowl21Awarded())){ 
			return Action.EndGame; 
		}

		if (bowl % 2 != 0) {	//First ball of frames 1-9
			//Strike in start of frame
			if (pins == 10) {
				bowl += 2;
				return Action.EndTurn;
			}
			bowl++;
			return Action.Tidy;
		} 
		else if(bowl % 2 == 0) {	//Second ball of frames 1-9
			bowl++;
			return Action.EndTurn;
		}
			
		throw new UnityException ("Exception: No clue what to do");
	}

	private bool Bowl21Awarded(){
		return(bowls [19-1] + bowls [20-1] >= 10);
	}



}
