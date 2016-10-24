using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Revolver : AttackBank{
	public float speed;

	public class Slot{
		bool occupied = true;
		Image i;
		int color;
	}

	Slot slot1;
	Slot slot2;
	Slot slot3;

	CharacterNervousSystem cns;
	void Start(){
		cns = GameObject.FindGameObjectWithTag ("Player").GetComponent<CharacterNervousSystem> ();
	}

	void PrimeShot(){
		cns.attackPrimed = true;
		Debug.Log (cns.attackPrimed);
	}

	public override void NextAttack(Transform target){
		Attack.CreateAttack (target, attacks [currentAttack], damage[currentAttack], effectBool[currentAttack], effectAmount[currentAttack], effectTime[currentAttack]);
		currentAttack++;
		if (currentAttack == attacks.Length) {
			currentAttack = 0;
		}
		StartCoroutine("Revolve");
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
		PrimeShot ();
	}

}
