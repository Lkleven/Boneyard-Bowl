using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
	public Vector3 launchVelocity;
	public bool inPlay = false;
	public Material[] materials;

	private Renderer rend;

	private Rigidbody rb;
	private AudioSource audioSource;
	private Vector3 ballStartPosition;
	private bool resetGutterBall = false, resetBall = false, inPinSetterCollider = false;
	private float gutterBallResetTime = 3f, normalBallResetTime = 3f;				//Change this? Change update in Update()
	private float timePerTurnAfterBallLaunch = 20f;									//Change this?, Change update in Reset()
	private GameManager gameManager;



	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer> ();
		rend.enabled = true;

		gameManager = GameObject.FindObjectOfType<GameManager> ();
		rb = GetComponent<Rigidbody> ();
		rb.useGravity = false;
		audioSource = GetComponent<AudioSource> ();
		ballStartPosition = transform.position;
	}

	void Update(){

		//Handles gutterballs
		if (resetGutterBall) {
			gutterBallResetTime -= Time.deltaTime;
			if (gutterBallResetTime < 0f) {
				gameManager.Bowl (0);				//Treated as a bowl of 0
				Reset ();
				gutterBallResetTime = 3f;
				resetGutterBall = false;
			}
		}

		//Normal Ball Resets
		if (resetBall) {
			normalBallResetTime -= Time.deltaTime;
			if (normalBallResetTime < 0f) {
				Reset ();
				normalBallResetTime = 3f;
				resetBall = false;
			}
		}

		//Resets ball if its too slow to know down any pins or leave the play area within a timeframe
		if (inPlay) {
			timePerTurnAfterBallLaunch -= Time.deltaTime;
			if (timePerTurnAfterBallLaunch < 0f){
				Reset ();
				Debug.Log ("Ball Timeout");
				gameManager.Bowl (0);					//Treated as a bowl of 0
			}
		}
	}

	public void Launch (Vector3 velocity){
		rb.useGravity = true;
		rb.velocity = velocity;
		inPlay = true;
	}

	void OnTriggerEnter(Collider collider){
		if(collider.name.Equals("LaneCollider")){
			audioSource.Play();
		}
		if(collider.name.Equals("PinSetter")){
			inPinSetterCollider = true;
		}

	}

	void OnTriggerExit(Collider collider){
		if(collider.name.Equals("LaneCollider")){
			audioSource.Stop();
		}

		if(collider.name.Equals("PinSetter")){
			resetBall = true;
		}

		//Ball leaves the playarea before it hits the pin area box
		if(collider.name.Equals("PlayAreaCollider") && !inPinSetterCollider){
			resetGutterBall = true;
		}
	}

	//Resets the ball
	public void Reset(){
		inPlay = false;
		inPinSetterCollider = false;
		timePerTurnAfterBallLaunch = 20f;			//resets the launchBallResetTimer
		rb.useGravity = false;
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
		transform.position = ballStartPosition;
		transform.rotation = Quaternion.identity;
		//cameraControl.ResetCameraPosition ();
	}

	//Changes the material of the ball relating to current player
	public void SetPlayerMaterial(int playerNumber){
		rend.sharedMaterial = materials [playerNumber - 1];
	}
}
