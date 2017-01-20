using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Revolver : AttackTransmitter {
	public bool primed = true;
	public float speed = 1.0f;
	public Transform prush;
	public static Revolver revolver;



	public void Awake(){
		revolver = this;
		prush = GameObject.FindGameObjectWithTag ("Prush").transform;
	}

	public new void Attack(Transform target){
		attackTarget = target;
		if (primed) {
			primed = false;
			NextAttack ();
		}
	}
	public new void Attack(){
		if (primed) {
			primed = false;
			NextAttack ();
		}
	}


	public new void NextAttack(){
		StartCoroutine ("Revolve");
		CreateAttack (currentAttack, prush);
		currentAttack++;
		if (currentAttack >= attacks.Length) {
			currentAttack = 0;
		}
	}

	public IEnumerator Revolve(){
		int oldAngle = (int) transform.rotation.eulerAngles.z; 
		int newAngle = oldAngle - 120;
		float currentAngle = oldAngle;
		float timer = 0.0f;
		if (newAngle == -360){
			newAngle = 0;
		}
		while ((int) currentAngle != newAngle) {
			timer += Time.deltaTime;
			currentAngle = Mathf.SmoothStep (oldAngle, newAngle, timer/speed);
			transform.eulerAngles = new Vector3(0, 0, currentAngle);
			if ((int)currentAngle == -360) {
				currentAngle = 0;
			}
			yield return null;
		}
		transform.eulerAngles = new Vector3(0, 0, newAngle);
		primed = true;
	}

	public void SetSpeed(float s){
		speed = s;
	}

}