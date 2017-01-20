using UnityEngine;
using System.Collections;
using UnityEngine.UI;
[RequireComponent (typeof (RigidbodyInterface))]
[RequireComponent (typeof (Brain))]

public class CentralNervousSystem : MonoBehaviour {

	RigidbodyInterface ri;
	public int health = 100;
	public AttackColor colorWeakness;
	public float colorDamageMultiplier = 1.0f;
	public DeathScript deathScript;
	public Floater billboard;
	public GroundCollider groundCollider;
	public bool grounded = true;

	public float xInput;
	public float yInput;
	void Awake(){
		ri = GetComponent<RigidbodyInterface>();
		billboard = GetComponentInChildren<Floater> ();
	}

	void Start(){
		deathScript = GetComponent<DeathScript> ();
	}

	public float GetHealth(){
		return health;
	}
	public void SetHealth(int h){
		health = h;
	}
	public void ReduceHealth(float h){
		health -= (int) h;
		if(health <= 0){
			Die ();
		}
	}
	public void ReduceHealth(float h, AttackColor color){
		health -= (int) h;
		if(health <= 0){
			Die ();
		}
	}
	public void PostMessage(string s, Color c){
		billboard.NewMessage (s, c);
	}
	public void PostMessage(string s){
		billboard.NewMessage (s);
	}

	public void SetSpeed(float s){
		ri.maxHorizontalSpeed = s;//change speed and then update input
		SetxInput (xInput);
	}
	public float GetSpeed(){
		return ri.maxHorizontalSpeed;
	}

	public void SetxInput(float f){
			xInput = f;
			ri.SetxInput (xInput);
	}
	public void SetyInput(float f){
		if (f != yInput){
			yInput = f;	
			ri.SetyInput (yInput);
	}
}
	public void Die(){
		if (deathScript != null) {
			deathScript.Die ();
		}
	}
}
