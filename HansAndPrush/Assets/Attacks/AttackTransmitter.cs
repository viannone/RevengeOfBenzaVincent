using UnityEngine;
using System.Collections;

public class AttackTransmitter : MonoBehaviour {

	public Transform attackTarget;
	public int currentAttack;
	public int coolDownTime;
	public float timer = 0.0f;
	public Transform[] attackPrefabs;
	public Attack.AttackColor[] colors;
	public int[] values;
	public bool[] effects;
	public float[] effectValues;
	public int[] effectTimes;

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
		if (currentAttack >= attackPrefabs.Length) {
			currentAttack = 0;
		}
	}

	public void CreateAttack(int i){
		Transform a = (Transform) Instantiate (attackPrefabs [i], transform.position, Quaternion.identity);
		if (attackTarget != null) {
			a.GetComponent<Attack> ().SetUp (transform, colors [i], values [i], effects [i], effectValues [i], effectTimes [i], attackTarget);
		} else {
			a.GetComponent<Attack> ().SetUp (transform, colors [i], values [i], effects [i], effectValues [i], effectTimes [i]);
		}
	}

	public void CreateAttack(int i, Transform origin){
		Transform a = (Transform) Instantiate (attackPrefabs [i], transform.position, Quaternion.identity);
		if (attackTarget != null) {
			a.GetComponent<Attack> ().SetUp (origin, colors [i], values [i], effects [i], effectValues [i], effectTimes [i], attackTarget);
		} else {
			a.GetComponent<Attack> ().SetUp (origin, colors [i], values [i], effects [i], effectValues [i], effectTimes [i]);
		}
	}
}
