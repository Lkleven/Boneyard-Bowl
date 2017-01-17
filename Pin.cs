using UnityEngine;
using System.Collections;

public class Pin : MonoBehaviour {
	public float standingTreshold = 0.5f;

	// Use this for initialization
	void Start () {
	
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
}
