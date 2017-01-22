using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Sequencer : MonoBehaviour {

	public CentralNervousSystem cns;
	public RigidbodyInterface ri;
	public SpriteRenderer r;

	public int run;
	public int walk;

	public void Awake(){
		cns = gameObject.GetComponent<CentralNervousSystem> ();
		r = gameObject.GetComponent<SpriteRenderer> ();
		ri = gameObject.GetComponent<RigidbodyInterface> ();
	}

	public void Start(){
		currentSequence.Play ();
	}
	public Seq[] allSequences;
	public Seq currentSequence;

	public void MapSequences(){
		allSequences = GetComponents<Seq> ();
		for (int i = 0; i < allSequences.Length; i++) {
			Debug.Log ("Sequence " + i + " is " + allSequences [i].folder);
		}
	}

	public void SetCurrentSequence(Seq s){
		StopCoroutine ("ManageAnimations");
		Debug.Log ("Now Playing: " + s.folder);
		currentSequence = s;
		StartCoroutine ("ManageAnimations");
	}

	public void Cue(Seq s){
		currentSequence.nextAnimation = s;
		//find transition frames to s
		Pivot[][] pivots = currentSequence.pivots;
		int count = 0;
		if (pivots != null) {
			for (int type = 0; type < pivots.Length; type++) {
				if (allSequences[pivots [type] [0].toSeq] == s) {
					for (int pivot = 0; pivot < pivots [type].Length; pivot++) {
						currentSequence.TerminateFrames (pivots [type] [pivot]);
						count++;
					}
					break;
				}
			}
		}
		if(count == 0){
			Debug.LogWarning("No Transitions from: " + currentSequence.folder + " to: " + s.folder);
			currentSequence.Stop();
			s.Play();
		}
	}

	public IEnumerator ManageAnimations(){
		int maxFrameRate = currentSequence.maxFrameRate;
		int minFrameRate = currentSequence.minFrameRate;
		float oldX = ri.currentXvelocity;
		while (true) {
			float xVel = ri.currentXvelocity;

			if (xVel < 0) {
				r.flipX = true;
			} else {
				r.flipX = false;
			}

			if ((oldX < 0 && xVel > 0)) {
				Cue (allSequences[walk]);
			} else if ((oldX > 0 && xVel < 0)) {
				Cue (allSequences[run]);
			}
				oldX = xVel;
			if (currentSequence.speedSensitive) {
				int frameRate = Mathf.RoundToInt ((Mathf.Abs (ri.currentXvelocity / ri.maxHorizontalSpeed) * maxFrameRate));
				if (frameRate < minFrameRate) {
					frameRate = minFrameRate;
				}
				currentSequence.ChangeFrameRate (frameRate);
			}
			yield return null;
		}
	}

}