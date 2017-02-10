using UnityEngine;
using System.Collections;

public class Pin : MonoBehaviour {
	public float standingTreshold = 3f;
	public float distanceToRaise = 40f;

	private AudioSource audioSource;


	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();
	}

	//returns true if the pin is currently standing within a treshold
	public bool IsStanding(){
		Vector3 rotation = transform.rotation.eulerAngles;
		float tiltInX = Mathf.Abs (rotation.x);
		float tiltInZ = Mathf.Abs (rotation.z);

		if (tiltInX < standingTreshold && tiltInZ < standingTreshold && transform.position.y > 19.5 && transform.position.y < 20.5 ) {
			return true;
		}
		return false;
	}

	//raises the pin if its standing. Part of the "tidy" animation
	public void RaiseIfStanding(){
		Rigidbody rigb = GetComponent<Rigidbody> ();
		rigb.useGravity = false;
		if (IsStanding ()) {
			transform.rotation = Quaternion.Euler(0,180,0);	//Make sure the pins get lowered at a perfect angle
			transform.Translate (new Vector3 (0, distanceToRaise, 0), Space.World);
		}
	}

	//lower all pins
	public void Lower(){
		transform.Translate (new Vector3 (0, -distanceToRaise, 0), Space.World);
		Rigidbody rigb = GetComponent<Rigidbody> ();
		rigb.useGravity = true;
	}

	void OnCollisionEnter(Collision other){
		GameObject thingHit = other.gameObject;

		if(thingHit.GetComponent<Pin>() || thingHit.GetComponent<Ball> ()){
			audioSource.Play ();
		}
	}
}
