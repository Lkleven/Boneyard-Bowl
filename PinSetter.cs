using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PinSetter : MonoBehaviour {
	public Text pinCount;
	public GameObject pinWheel,pinSet;
	public int lastStandingCount = -1;

	private Ball ball;
	private bool ballEnteredBox = false;
	private float lastChangeTime;
	private int lastSettledCount = 10;
	private ActionMaster actionMaster;

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
		int pinCount = lastSettledCount - CountStanding ();	//pinCount counts standing pins for scoring
		lastSettledCount = CountStanding ();				//Updates lastSettledCount to current standing pins

		ball.Reset();
		lastStandingCount = -1; //Indicates pins have settled, and ball not back in box
		ballEnteredBox = false;
	}

	void PinsFallen(){
		
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
		RemoveEmptyPinContainers ();
		GameObject newPins = Instantiate (pinSet);
		newPins.transform.position += new Vector3 (0, 40, 0);	//instantiate at the same height as distanceToRaise in Pin.cs due to lowering them again instantly
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
