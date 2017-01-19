using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	public Ball ball;

	private Vector3 offset;

	// Use this for initialization
	void Start () {
		offset = transform.position - ball.transform.position;
	
	}
	
	// Update is called once per frame
	void Update () {
		if (ball.transform.position.z <= 1650f) {				//z.position of the headpin is at 1879
			transform.position = ball.transform.position + offset;
		}
	}
}
