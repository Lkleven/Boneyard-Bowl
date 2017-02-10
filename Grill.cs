using UnityEngine;
using System.Collections;

public class Grill : MonoBehaviour {
	private AudioSource audioSource;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//Destroys pins leaving the pinSetter GameObject Collider.
	void OnTriggerEnter(Collider collider){
		GameObject thingLeft = collider.gameObject;
		if (thingLeft.GetComponent<Pin> ()) {
			audioSource.Play ();
			Destroy (thingLeft);
		}
	}
}
