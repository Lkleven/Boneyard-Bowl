using UnityEngine;
using System.Collections;

public class Pin : MonoBehaviour {
	public float standingTreshold = 0.5f;
	public float distanceToRaise = 40f;

	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		Rigidbody rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool IsStanding(){
		Vector3 rotation = transform.rotation.eulerAngles;
		float tiltInX = Mathf.Abs (rotation.x);
		float tiltInZ = Mathf.Abs (rotation.z);

		if (tiltInX < standingTreshold && tiltInZ < standingTreshold && transform.position.y > 19.5 && transform.position.y < 20.5 ) {
			return true;
		}
		return false;
	}

	public void RaiseIfStanding(){
		Rigidbody rigb = GetComponent<Rigidbody> ();
		rigb.useGravity = false;
		if (IsStanding ()) {
			transform.Translate (new Vector3 (0, distanceToRaise, 0), Space.World);
		}
	}

	public void Lower(){
		transform.Translate (new Vector3 (0, -distanceToRaise, 0), Space.World);
		Rigidbody rigb = GetComponent<Rigidbody> ();
		rigb.useGravity = true;
	}
}
