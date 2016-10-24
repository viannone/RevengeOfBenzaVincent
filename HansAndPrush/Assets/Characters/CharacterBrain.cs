using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CharacterNervousSystem))]
public class CharacterBrain : MonoBehaviour {

	public float xInput;
	public float yInput;
	public float aInput;
	public float pauseInput;

	public CharacterNervousSystem cns;
	public void Start() {
		cns = GetComponent<CharacterNervousSystem> ();
	}
	public virtual void Update() {
		xInput = Input.GetAxis ("Horizontal");
		yInput = Input.GetAxis ("Vertical");
		aInput = Input.GetAxis ("Action1");
		cns.SetxInput (xInput);
		cns.SetyInput (yInput);
		cns.SetaInput (aInput);
	}
}
