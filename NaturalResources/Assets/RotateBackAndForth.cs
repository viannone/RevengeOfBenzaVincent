using UnityEngine;
using System.Collections;

public class RotateBackAndForth : MonoBehaviour {
	public float rAmount;
	public float rRate;
	float rrr;
	float time;
	// Update is called once per frame
	void Update () {
		time = Time.timeSinceLevelLoad;
		rrr = rAmount * (Mathf.Sin (time * rRate));
		transform.RotateAround (transform.position, Vector3.forward, rrr);

	}


}