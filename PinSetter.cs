using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PinSetter : MonoBehaviour {
	public Text pinCount;
	public GameObject pinWheel;
	public int lastStandingCount = -1;

	private Ball ball;
	private bool ballEnteredBox = false;
	private float lastChangeTime;

	// Use this for initialization
	void Start () {
		pinCount.text = CountStanding().ToString ();
		ball = GameObject.FindObjectOfType<Ball> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (ballEnteredBox) {
			pinCount.text = CountStanding().ToString ();
			pinWheel.transform.Rotate ((Vector3.forward * Time.deltaTime) * -180); //wheel rotates 50 degrees along the z axis per second
			CheckStanding();
		}
	}

	void CheckStanding(){
		int currentStanding = CountStanding ();
		if (currentStanding != lastStandingCount) {
			lastChangeTime = Time.time;
			lastStandingCount = currentStanding;
			return;
		}

		float settleTime = 3f; //How long to waint to consider pins settled
		if((Time.time - lastChangeTime) > settleTime){
			PinsHaveSettled ();
		}

	}

	void PinsHaveSettled(){
		ball.Reset();
		lastStandingCount = -1; //Indicates pins have settled, and ball not back in box
		ballEnteredBox = false;

	}

	int CountStanding(){
		int standing = 0;
		foreach (Pin pin in GameObject.FindObjectsOfType<Pin>()) {
			if (pin.IsStanding ()) {
				standing++;
			}
		}
		return standing;
	}

	void OnTriggerExit(Collider collider){
		GameObject thingLeft = collider.gameObject;
		if (thingLeft.GetComponent<Pin> ()) {
			Destroy (thingLeft);
		}
	}

	void OnTriggerEnter(Collider collider){
		if (collider.GetComponent<Ball> ()) {
			ballEnteredBox = true;
		}
	}

	public void RaisePins(){
		foreach (Pin pin in GameObject.FindObjectsOfType<Pin>()) {
			pin.RaiseIfStanding ();
		}
	}

	public void LowerPins(){
		foreach (Pin pin in GameObject.FindObjectsOfType<Pin>()) {
			pin.Lower ();
		}
	}



	public void RenewPins(){
		print ("renew");
	}
}
