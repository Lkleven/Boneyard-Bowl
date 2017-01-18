﻿using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
	public Vector3 launchVelocity;
	public bool inPlay = false;

	private Rigidbody rb;
	private AudioSource audioSource;
	private Vector3 ballStartPosition;
	//private CameraControl cameraControl;


	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		rb.useGravity = false;
		audioSource = GetComponent<AudioSource> ();
		ballStartPosition = transform.position;
		//cameraControl = GameObject.FindObjectOfType<CameraControl>();
	}

	public void Launch (Vector3 velocity){
		rb.useGravity = true;
		rb.velocity = velocity;
		//audioSource.Play ();
		inPlay = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//rb.AddForce(transform.forward * -speed);
	
	}

	void OnTriggerEnter(Collider collider){
		if(collider.name.Equals("Lane")){
			audioSource.Play();
		}
	}

	public void Reset(){
		inPlay = false;
		rb.useGravity = false;
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
		transform.position = ballStartPosition;
		//cameraControl.ResetCameraPosition ();
	}
}
