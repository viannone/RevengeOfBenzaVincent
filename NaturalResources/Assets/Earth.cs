using UnityEngine;
using System.Collections;

public class Earth : MonoBehaviour {
	public float rAmount = 1.0f;
	public void Update(){
		float rrr = rAmount * Time.deltaTime;
		transform.RotateAround (transform.position, Vector3.forward, rrr);
	}
}
