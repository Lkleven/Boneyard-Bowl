using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
	public Vector3 launchVelocity;
	public bool inPlay = false;

	private Rigidbody rb;
	private AudioSource audioSource;


	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		rb.useGravity = false;
		audioSource = GetComponent<AudioSource> ();
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
}
