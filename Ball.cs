using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
	public Vector3 launchVelocity;

	private Rigidbody rb;
	private AudioSource audioSource;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		audioSource = GetComponent<AudioSource> ();
		Launch ();
	}

	public void Launch (){
		rb.velocity = launchVelocity;
		audioSource.Play ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//rb.AddForce(transform.forward * -speed);
	
	}
}
