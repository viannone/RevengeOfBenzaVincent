using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour{
	public enum AttackColor {BLUE, RED, GREEN, PURPLE, WHITE, BLACK, BROWN, YELLOW};
	public Transform origin;
	public AttackColor attackColor;
	public int value;
	public bool effect;
	public float effectValue;
	public int effectTime;
	public Transform target;
	public void SetUp(Transform origin, Attack.AttackColor color, int value, bool effect, float effectValue, int effectTime, Transform target){
		this.origin = origin;
		attackColor = color;
		this.value = value;
		this.effect = effect;
		this.effectValue = effectValue;
		this.effectTime = effectTime;
		this.target = target;
		gameObject.layer = origin.gameObject.layer + 1;
	}
	public void SetUp(Transform origin, Attack.AttackColor color, int value, bool effect, float effectValue, int effectTime){
		this.origin = origin;
		attackColor = color;
		this.value = value;
		this.effect = effect;
		this.effectValue = effectValue;
		this.effectTime = effectTime;
		gameObject.layer = origin.gameObject.layer + 1;
	}
	public IEnumerator CountDownToSelfDestruct(float f){
		yield return new WaitForSeconds (f);
		SelfDestruct ();
	}
	public void SelfDestruct(){
		Destroy (gameObject);
	}
}
