using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))] 
[RequireComponent (typeof (CharacterNervousSystem))]
public class CharacterMovementInterface : MonoBehaviour {

	Rigidbody2D r;
	//this is just meant for redundancy with the built-in kinematic control
	private float x;
	private float y;
	//this script is akin to the "muscles" of a body
	//other scripts work through it to move the characters around


	void Awake () {
		r = transform.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	public void SetXInput(float x){
		r.velocity = new Vector2 (x, r.velocity.y);
	}
	public void SetYInput(float y){
		r.velocity = new Vector2 (r.velocity.x, y);
	}
}
