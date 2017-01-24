using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PinSetter : MonoBehaviour {
	public Text pinCount;
	public GameObject pinWheel,pinSet;

	private int lastStandingCount = -1;
	private bool ballEnteredBox = false;
	private float lastChangeTime;
	private int lastSettledCount = 10;
	private ActionMaster actionMaster = new ActionMaster();

	private Ball ball;
	private Animator animator;

	// Use this for initialization
	void Start () {
		ball = GameObject.FindObjectOfType<Ball> ();
		animator = GetComponent<Animator> ();
		pinCount.text = CountStanding().ToString ();
	}
	
	// Update is called once per frame
	void Update () {
		if (ballEnteredBox) {
			pinCount.text = CountStanding().ToString ();
			pinWheel.transform.Rotate ((Vector3.forward * Time.deltaTime) * -180); //wheel rotates 50 degrees along the z axis per second
			UpdateStandingCountAndSettle();
		}
	}


	void UpdateStandingCountAndSettle(){
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
		int standing = CountStanding ();
		int pinFall = lastSettledCount - standing;	//pinFall stores the amount standing pins for scoring
		lastSettledCount = standing;				//Updates lastSettledCount to current standing pins
		PinMachine(pinFall);
		ball.Reset();
		lastStandingCount = -1; //Indicates pins have settled, and ball not back in box
		ballEnteredBox = false;
	}

	public void PinMachine(int pins){
		ActionMaster.Action action = actionMaster.Bowl (pins);
		Debug.Log ("pins down: " + pins + " - " + action);

		if (action == ActionMaster.Action.Tidy) { 
			animator.SetTrigger ("tidyTrigger");
		}
		else if (action == ActionMaster.Action.Reset || action == ActionMaster.Action.EndTurn || action == ActionMaster.Action.EndGame){ 
			animator.SetTrigger("resetTrigger");
			lastSettledCount = 10;		//Resets to 10 pins standing
		}
		
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


	//Destroys pins leaving the pinSetter GameObject Collider.
	void OnTriggerExit(Collider collider){
		GameObject thingLeft = collider.gameObject;
		if (thingLeft.GetComponent<Pin> ()) {
			Destroy (thingLeft);
		}
		if (thingLeft.GetComponent<Ball> ()) {
			ballEnteredBox = true;
		}

	}

	/*void OnTriggerEnter(Collider collider){
		if (collider.GetComponent<Ball> ()) {
			ballEnteredBox = true;
		}
	}*/

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
		RemoveEmptyPinContainers ();
		GameObject newPins = Instantiate (pinSet);
		newPins.transform.position += new Vector3 (0, 40, 0);	//instantiate at the same height as distanceToRaise in Pin.cs due to lowering them again instantly
		pinCount.text = "10";									// resets UI counter at 10 pins
	}

	private void RemoveEmptyPinContainers(){
		GameObject[] pinContainers = GameObject.FindGameObjectsWithTag ("PinContainer");
		foreach (GameObject pinContainer in pinContainers) {
			if (pinContainer.transform.childCount == 0) {
				Destroy (pinContainer);
			}
		}
	}
}
