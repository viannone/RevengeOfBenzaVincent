using UnityEngine;
using System.Collections;

public class Attack {
	[Header("COLORS: Blue 0, R 1, G 2, P 3, W 4, Black 5, Br 6, Y7")]
	public int color;
	public int value;
	public bool effect;
	public float effectValue;
	public float effectTime;
	public Transform target;

	public static void CreateAttack(Transform target, int color, int value, bool effect, float effectValue, float effectTime){
		Attack a = new Attack ();
		a.color = color;
		a.value = value;
		a.effect = effect;
		a.effectValue = effectValue;
		a.effectTime = effectTime;
		a.target = target;
		target.GetComponent<CharacterNervousSystem> ().GetAttacked (a);
	}

	private Attack(){}
}
