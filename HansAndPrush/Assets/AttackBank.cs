using UnityEngine;
using System.Collections;

public class AttackBank : MonoBehaviour {
	public int currentAttack = 0;
	[Header("COLORS: Blue 0, R 1, G 2, P 3, W 4, Black 5, Br 6, Y7")]
	public int[] attacks;

	[Space(10)]
	public int blueDamage;
	public bool blueEffect;
	public float blueEffectAmount;
	public float blueEffectTime;
	[Space(10)]
	public int redDamage;
	public bool redEffect;
	public float redEffectAmount;
	public float redEffectTime;
	[Space(10)]
	public int greenDamage;
	public bool greenEffect;
	public float greenEffectAmount;
	public float greenEffectTime;
	[Space(10)]
	public int purpleDamage;
	public bool purpleEffect;
	public float purpleEffectAmount;
	public float purpleEffectTime;
	[Space(10)]
	public int whiteDamage;
	public bool whiteEffect;
	public float whiteEffectAmount;
	public float whiteEffectTime;
	[Space(10)]
	public int blackDamage;
	public bool blackEffect;
	public float blackEffectAmount;
	public float blackEffectTime;
	[Space(10)]
	public int brownDamage;
	public bool brownEffect;
	public float brownEffectAmount;
	public float brownEffectTime;
	[Space(10)]
	public int yellowDamage;
	public bool yellowEffect;
	public float yellowEffectAmount;
	public float yellowEffectTime;

	int[] damage;
	bool[] effectBool;
	float[] effectAmount;
	float[] effectTime;
	void Start(){
		damage = new int[] {
		blueDamage,
		redDamage,
		greenDamage,
		purpleDamage,
		whiteDamage,
		blackDamage,
		brownDamage,
		yellowDamage
	};
		effectBool = new bool[] {
		blueEffect,
		redEffect,
		greenEffect,
		purpleEffect,
		whiteEffect,
		blackEffect,
		brownEffect,
		yellowEffect
	};
		effectAmount = new float[]{
		blueEffectAmount,
		redEffectAmount,
		greenEffectAmount,
		purpleEffectAmount,
		whiteEffectAmount,
		blackEffectAmount,
		brownEffectAmount,
		yellowEffectAmount,
	};
		effectTime = new float[]{
		blueEffectTime,
		redEffectTime,
		greenEffectTime,
		purpleEffectTime,
		whiteEffectTime,
		blackEffectTime,
		brownEffectTime,
		yellowEffectTime,
	};
	}

	public void NextAttack(Transform target){
		Attack.CreateAttack (target, attacks [currentAttack], damage[currentAttack], effectBool[currentAttack], effectAmount[currentAttack], effectTime[currentAttack]);
		currentAttack++;
		if (currentAttack == attacks.Length) {
			currentAttack = 0;
		}
	}
}
