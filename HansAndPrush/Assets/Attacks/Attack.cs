using UnityEngine;
using System.Collections;

public enum AttackColor {BLUE, RED, GREEN, PURPLE, WHITE, BLACK, BROWN, YELLOW};
public enum Effect {DOT, SLOW, SOMETHING};
[System.Serializable]
public class Attack {
	public Transform prefab;
	public AttackColor attackColor;
	public int value;
	public Effects[] effects;
}
[System.Serializable]
public struct Effects{
	public Effect effect;
	public float effectValue;
	public float effectTime;
}
