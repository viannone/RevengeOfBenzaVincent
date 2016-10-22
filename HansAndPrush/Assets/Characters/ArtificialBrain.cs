using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CharacterNervousSystem))]
public class ArtificialBrain : CharacterBrain {
	public Transform target;

	public void Start() {
		cns = GetComponent<CharacterNervousSystem> ();
	}
	public override void Update() {
		if (target.position.x - transform.position.x > 1) {
			this.xInput = 1.0f;
		}else if(target.position.x - transform.position.x < - 1){
			this.xInput = -1.0f;
		}
		cns.SetxInput (this.xInput);
		cns.SetyInput (this.yInput);
		cns.SetaInput (this.aInput);
	}


}
