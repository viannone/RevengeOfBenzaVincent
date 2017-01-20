using UnityEngine;
using System.Collections;

public class AttackTransmitter : MonoBehaviour {

	public Transform attackTarget;
	public int currentAttack;
	public int coolDownTime;
	public float timer = 0.0f;

	public Attack[] attacks;

	public void Attack(Transform target){
		attackTarget = target;
		timer += Time.deltaTime;
		if (timer >= coolDownTime) {
			timer = 0.0f;
			NextAttack ();
		}
	}
	public void Attack(){
		timer += Time.deltaTime;
		if (timer >= coolDownTime) {
			timer = 0.0f;
			NextAttack ();
		}
	}

	public void NextAttack(){
		CreateAttack (currentAttack);
		currentAttack++;
		if (currentAttack >= attacks.Length) {
			currentAttack = 0;
		}
	}

	public void CreateAttack(int i){
		Transform a = (Transform) Instantiate (attacks [i].prefab, transform.position, Quaternion.identity);
		a.GetComponent<AttackPrefab> ().SetUp(transform, attackTarget, attacks[i]);
	}
	public void CreateAttack(int i, Transform altOrigin){
		Transform a = (Transform) Instantiate (attacks [i].prefab, altOrigin.position, Quaternion.identity);
		a.GetComponent<AttackPrefab> ().SetUp(altOrigin, attackTarget, attacks[i]);
	}
}
