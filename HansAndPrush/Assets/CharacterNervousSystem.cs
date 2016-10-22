using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[RequireComponent (typeof (CharacterMovementInterface))]
[RequireComponent (typeof (CharacterBrain))]
[RequireComponent (typeof (BoxCollider2D))]
public class CharacterNervousSystem : MonoBehaviour {
	private CharacterMovementInterface cmi;
	public BoxCollider2D groundCollider;
	public AttackBank attackBank;

	//public for debug reasons only
	private float xInput;
	private float yInput;
	public float aInput; //ATTACK
	public Transform target;

	//Billboard
	public Color defaultColor = Color.red;
	public Text billboard;
	public float xSpeed;
	public float ySpeed;
	//jumping
	public bool isJumpingCharacter;
	public float jumpRefreshTime = 1.0f;
	public float jumpRefreshTimer = 0.0f;
	public float attackRefreshTime = 1.0f;
	public float attackRefreshTimer = 0.0f;
	//effects
	public float xSpeedModifier = 1.0f;
	public float ySpeedModifier = 1.0f;
	public float damageModifier = 1.0f;
	//color
	[Header("COLORS: Blue 0, R 1, G 2, P 3, W 4, Black 5, Br 6, Y7")]
	int color;
	int colorWeakness;
	public string[] attacks = new string[3];
	float colorDamageModifier = 1.0f;
	public float health = 1000.0f;


	void Start () {
		jumpRefreshTime = 1.0f;
		cmi = GetComponent<CharacterMovementInterface> ();
		StartCoroutine ("ProcessInput");
	}
	public void GetAttacked (Attack a){
		int incomingColor = a.color;
		if (incomingColor == colorWeakness) {
			TakeDamage ((a.value * colorDamageModifier));
		} else {
			TakeDamage (a.value * damageModifier);
		}
		if (a.effect == true) {
			if (incomingColor == 1) {//RED
				StartCoroutine (DOT (a.effectValue, a.effectTime));
			} else if (incomingColor == 0) {//BLUE
				StartCoroutine (Slow (a.effectValue, a.effectTime));
			} else if (incomingColor == 7) {//YELLOW
				StartCoroutine (Amplify (a.effectValue, a.effectTime));
			}
		}
	}
	public void AttackTarget(){
		Debug.Log ("Attacking: " + target);
		attackBank.NextAttack (target);
	}
	public IEnumerator ProcessInput(){
		while (true) {
			cmi.SetXInput (xInput * xSpeed * xSpeedModifier);
			if(yInput > 0){
				if (GroundCheck()) {
				if (jumpRefreshTimer >= jumpRefreshTime) {
					jumpRefreshTimer = 0.0f;
						cmi.SetYInput (yInput * ySpeed * ySpeedModifier);
				}
			}
			}if (aInput > 0) {
				if (attackRefreshTimer >= attackRefreshTime) {
					AttackTarget ();
					attackRefreshTimer = 0.0f;
				}
			}
			float t = Time.deltaTime;
			jumpRefreshTimer += t;
			attackRefreshTimer += t;
			yield return new WaitForFixedUpdate ();
		}
	}

	public bool GroundCheck(){
		if(groundCollider.IsTouchingLayers(0)){
			Debug.Log ("Grounded");
			return true;
		}else{
			Debug.Log("Not Grounded");
			return false;
		}
	}

	//EFFECTS
	public void TakeDamage(float value){
		health -= value;
		billboard.text = ((int) health).ToString();
	}
	public void TakeDamage(float value, string s){
		health -= value;
		Debug.Log (s);
		billboard.text = ((int) health).ToString();
	}
	public IEnumerator DOT(float effectValue, float effectTime){
		float timer = 0.0f;
		while (timer < effectTime) {
			billboard.color = Color.red;
			float t = Time.fixedDeltaTime;
			timer += t;
			TakeDamage (effectValue * t);
			yield return new WaitForFixedUpdate ();
		}
		billboard.color = defaultColor;
	}
	public IEnumerator Slow(float effectValue, float effectTime){
		float timer = 0.0f;
		xSpeedModifier = xSpeedModifier * effectValue;
		ySpeedModifier = xSpeedModifier * effectValue;
		Debug.Log ("Slowed");
		while (timer < effectTime) {
			float t = Time.fixedDeltaTime;
			timer += t;
			yield return new WaitForFixedUpdate ();
		}
		xSpeedModifier = xSpeedModifier/effectValue;
		ySpeedModifier = xSpeedModifier/effectValue;
		Debug.Log ("Unslowed. xSpeedModifier: " + xSpeedModifier);
	}
		
	public IEnumerator Amplify(float effectValue, float effectTime){
		float timer = 0.0f;
		damageModifier = damageModifier * effectValue;
		Debug.Log ("Amplified");
		while (timer < effectTime) {
			float t = Time.fixedDeltaTime;
			timer += t;
			yield return new WaitForFixedUpdate ();
		}
		damageModifier = damageModifier/effectValue;
		Debug.Log ("Damage UnAmplified. DamageModifier: " + damageModifier);
	}
	//these deterimine what the character "WANTS" to do; not what actually happens
	public void SetxInput(float x){
		xInput = x;
	}
	public void SetyInput(float y){
		yInput = y;
	}
	public void SetaInput(float a){
		aInput = a;
	}
}
