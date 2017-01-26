using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	private List<int> bowls = new List <int> ();
	private Ball ball;
	private PinSetter pinSetter;

	// Use this for initialization
	void Start () {
		ball = GameObject.FindObjectOfType<Ball> ();
		pinSetter = GameObject.FindObjectOfType<PinSetter> ();
	}

	public void Bowl (int pinFall){
		bowls.Add (pinFall);
		ActionMaster.Action nextAction = ActionMaster.NextAction (bowls);
		pinSetter.PinMachine (nextAction);
		ball.Reset ();
	}


}
