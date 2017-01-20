using UnityEngine;
using System.Collections;
[RequireComponent (typeof (CentralNervousSystem))]
public class DamageInput : MonoBehaviour {

	CentralNervousSystem cns;
	AttackColor colorWeakness;
	float colorDamageMultiplier = 1.0f;
	// Use this for initialization
	void Start () {
		cns = GetComponent<CentralNervousSystem> ();
		colorWeakness = cns.colorWeakness;
		colorDamageMultiplier = cns.colorDamageMultiplier;
	}

	public void TakeHit(Attack incomingAttack){
		//first just take the damage
		if (incomingAttack.attackColor == colorWeakness) {
			int amnt = (int) (colorDamageMultiplier * incomingAttack.value);
			cns.ReduceHealth (amnt);
			cns.PostMessage("Color Hit: " + amnt); 
		} else {
			cns.ReduceHealth (incomingAttack.value);
			cns.PostMessage ("Hit: " + incomingAttack.value);
		}

		foreach (Effects e in incomingAttack.effects) {//if there's an EFFECT that comes with the damage
			switch (e.effect) {
			case Effect.DOT:
				StartCoroutine (DOT (e.effectValue, e.effectTime));
				break;
			case Effect.SLOW:
				StartCoroutine (SLOW (e.effectValue, e.effectTime));
				break;
			}
		}
	}
	public IEnumerator DOT(float effectValue, float effectTime){
		int count = 0;
		while (count < effectTime) {
			yield return new WaitForSeconds(1.0f);
			cns.ReduceHealth (effectValue);
			cns.PostMessage ("BURNED: -" + effectValue, Color.red);
			count++;
		}
	}
	public IEnumerator SLOW(float effectValue, float effectTime){
		float timer = 0.0f;
		float oldSpeed = cns.GetSpeed ();
		float amount = effectValue * oldSpeed;
		cns.SetSpeed (oldSpeed - amount);
		cns.PostMessage ("SLOWED", Color.blue);
		while (timer <= effectTime) {
			timer += Time.deltaTime;
			yield return null;
		}
		cns.SetSpeed (cns.GetSpeed() + amount);
		cns.PostMessage ("Speed Restored", Color.blue);
	}

}
