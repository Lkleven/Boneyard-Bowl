using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class PinCounter : MonoBehaviour {
	public Text pinCount;
	public GameObject pinWheel;

	private int lastStandingCount = -1;
	private bool ballEnteredBox = false;
	private float lastChangeTime;
	private int lastSettledCount = 10;

	private GameManager gameManager;


	// Use this for initialization
	void Start () {
		gameManager = GameObject.FindObjectOfType<GameManager> ();
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

	public void Reset(){
		lastSettledCount = 10;
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
		gameManager.Bowl(pinFall);
	
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
		
	public void StartCounting(){
		ballEnteredBox = true;
	}
}
