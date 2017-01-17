using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Ball))]			//if there is no Ball script on the bowling ball, this will add the script to the game Object
public class DragLaunch : MonoBehaviour {
	private Ball ball;
	private Vector3 dragStart, dragEnd;
	private float startTime, endTime;

	// Use this for initialization
	void Start () {
		ball = GetComponent<Ball> ();
	
	}
	
	public void DragStart(){
		dragStart = Input.mousePosition;
		startTime = Time.time;

		//Capture time & position of drag start
	}

	public void DragEnd(){
		dragEnd = Input.mousePosition;
		endTime = Time.time;
		Vector3 dragChange = dragEnd - dragStart;
		float dragDuration = endTime - startTime;
		if(! ball.inPlay){
			ball.Launch (new Vector3(dragChange.x/dragDuration, 0, dragChange.y/dragDuration)); 	//translates drag in y direction to the z direction of ball velocity
		}
	}

	public void MoveStart(float xNudge){
		Vector3 currentPosition = ball.transform.position;
		if (!ball.inPlay && currentPosition.x + xNudge <= 45 && currentPosition.x + xNudge >= -45) { //clamps the ball within the width of the lane
			ball.transform.Translate (new Vector3 (xNudge, 0, 0));
		}
	}
}
