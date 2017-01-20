using UnityEngine;
using System.Collections;

public class AttackPrefab : MonoBehaviour {
	public Attack attack;
	public Transform origin;
	public Transform target;

	public void SetUp(Transform origin, Transform target, Attack a){
		gameObject.layer = origin.gameObject.layer + 1;
		attack = a;
		this.origin = origin;
		this.target = target;
	}

	public void SelfDestruct (){
		Destroy (this.gameObject);
	}

	public IEnumerator CountDownToSelfDestruct(float f){
		yield return new WaitForSeconds (f);
		SelfDestruct ();
	}
}
