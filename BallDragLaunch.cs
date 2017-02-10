using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Ball))]			//if there is no Ball script on the bowling ball, this will add the script to the game Object
public class BallDragLaunch : MonoBehaviour {
	private GameManager gameManager;
	private Ball ball;
	private Vector3 dragStart, dragEnd;
	private float startTime, endTime;

	// Use this for initialization
	void Start () {
		ball = GetComponent<Ball> ();
		gameManager = GameObject.FindObjectOfType<GameManager> ();
	}

	//Captures the start point of a drag movement
	public void DragStart(){
		if (gameManager.IsPinMachineBusy()) {
			return;
		} else {
			dragStart = Input.mousePosition;
			startTime = Time.time;
		}
	}

	//Captures and calculates the difference between the start and end point of a drag movement on the UI to calculate ball launch velocity
	public void DragEnd(){
		if (gameManager.IsPinMachineBusy ()) {
			return;
		} else {
			dragEnd = Input.mousePosition;
			endTime = Time.time;
			Vector3 dragChange = dragEnd - dragStart;
			float dragDuration = endTime - startTime;
			if (!ball.inPlay) {
				ball.Launch (new Vector3 (dragChange.x / dragDuration, 0, dragChange.y / dragDuration)); 	//translates drag in y direction to the z direction of ball velocity
			}
		}
	}

	//Allows the player to change the ball position before launching it
	public void MoveStart(float xNudge){
		Vector3 currentPosition = ball.transform.position;
		if (!ball.inPlay && currentPosition.x + xNudge <= 45 && currentPosition.x + xNudge >= -45) { //clamps the ball within the width of the lane
			ball.transform.Translate (new Vector3 (xNudge, 0, 0));
		}
	}
}
