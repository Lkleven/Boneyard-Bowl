using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PinSetter : MonoBehaviour {
	public GameObject pinSet;

	private PinCounter pinCounter;
	private Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		pinCounter = GameObject.FindObjectOfType<PinCounter> ();
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
			pinCounter.StartCounting();
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
		pinCounter.pinCount.text = "10";						// resets UI counter at 10 pins
	}

	private void RemoveEmptyPinContainers(){
		GameObject[] pinContainers = GameObject.FindGameObjectsWithTag ("PinContainer");
		foreach (GameObject pinContainer in pinContainers) {
			if (pinContainer.transform.childCount == 0) {
				Destroy (pinContainer);
			}
		}
	}

	public void PinMachine(ActionMaster.Action action){
		//Debug.Log ("pins down: " + pins + " - " + action);

		if (action == ActionMaster.Action.Tidy) { 
			animator.SetTrigger ("tidyTrigger");
		}
		else if (action == ActionMaster.Action.Reset || action == ActionMaster.Action.EndTurn || action == ActionMaster.Action.EndGame){ 
			animator.SetTrigger("resetTrigger");
			pinCounter.Reset ();		//Resets to 10 pins standing
		}
	}
}
