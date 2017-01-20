using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

[RequireComponent (typeof(Sequencer))]

public class Seq : MonoBehaviour {
	public string folder;
	public bool speedSensitive = false;
	public int frameRate = 30;
	public int minFrameRate;
	public int maxFrameRate;
	public int currentFrame = 0;
	public int[] transitionFrames;
	public Frame[] frames;
	public Seq nextAnimation;
	public Pivot[][] pivots;
	SpriteRenderer sr;
	Sequencer sequencer;

	public void Awake(){
		sr = GetComponent<SpriteRenderer> ();
		sequencer = GetComponent<Sequencer> ();
		CompileTransitions ();
	}
	public void Play(){
		StartCoroutine (IterateFrames ());
	}

	public void Play(int frame){
		currentFrame = frame;
		StartCoroutine (IterateFrames ());
	}
	public void Stop(){
		StopAllCoroutines ();
		Reset ();
	}
	public void ChangeFrameRate(int rate){
		frameRate = rate;
	}
	public void Reset(){
		foreach (Frame f in frames) {
			f.terminal = false;
		}
	}

	public void CompileTransitions(){
		Debug.Log ("Beginning... This may take a while");
		List<List<Pivot>> tempPivotList = new List<List<Pivot>> ();
		//initialize at least one list
		tempPivotList.Add (new List<Pivot> ());
		for(int f = 0; f < frames.Length; f++){
			foreach (Pivot p in frames[f].transitions) {
				p.fromFrame = f;
				bool listFound = false;
				foreach (List<Pivot> l in tempPivotList) {
					if (l.Count == 0) {
						l.Add (p);
						listFound = true;
						break;
					}else if (l [0] != null) {
						if (p.toSeq == l [0].toSeq) {
							l.Add (p);
							listFound = true;
							break;
						}
					}
				}
				if (!listFound) {
					List<Pivot> df = new List<Pivot> ();
					df.Add (p);
					tempPivotList.Add (df);
				}
			}
		}
		pivots = new Pivot[tempPivotList.Count][];
		for (int type = 0; type < tempPivotList.Count; type++) {
			pivots [type] = new Pivot[tempPivotList [type].Count];
			for (int pivot = 0; pivot < tempPivotList [type].Count; pivot++) {
				pivots [type] [pivot] = tempPivotList [type] [pivot];
			}
		}
		Debug.Log ("Phew, finally done. We have pivots going to " + pivots.Length + " different sequences");
		for (int type = 0; type < pivots.Length; type++) {
			Debug.Log ("<color=red>Type: " + type + ": " + pivots [type] [0].toSeq.folder + "</color>");
			for (int pivot = 0; pivot < tempPivotList [type].Count; pivot++){
				Debug.Log ("             <color=orange>From frame " + pivots[type][pivot].fromFrame + " Pivot to: " + pivots [type] [pivot].toSeq.folder + " at Frame: " + pivots [type] [pivot].atFrame + "</color>");
			}
		}
	}
	public void TerminateFrames(Pivot p){
		frames [p.fromFrame].terminal = true;
		frames [p.fromFrame].toFrame = p.atFrame;
	}

	public void GetFrames(){
		if (folder != null) {
			Debug.Log ("Getting Frames...");
			string s = "Sequences/" + folder;
			Object[] sprites = Resources.LoadAll (s, typeof(Sprite));
			Debug.Log (sprites.Length + " sprites at " + s);
			frames = new Frame[sprites.Length];
			for (int i = 0; i < frames.Length; i++) {
				frames [i] = new Frame ();
			}
			for (int i = 0; i < frames.Length; i++) {
				frames [i].frame = (Sprite) sprites [i];
			}
		} else {
			Debug.LogError ("No Path Specified");
		}
	}
	public IEnumerator IterateFrames(){
		sequencer.SetCurrentSequence (this);
		float timer = 0.0f;
		sr.sprite = frames[currentFrame].frame;
		int numFrames = frames.Length;
		while (true) {
			sr.sprite = frames [currentFrame].frame;
			float frameLength = (float) 1 / frameRate;
			timer += Time.deltaTime;
			if (timer >= frameLength) {
			/*	if (timer > frameLength * 2) {
					Debug.LogWarning ("Frame Repeated:" + currentFrame);
				}*/ 
				timer = timer % frameLength;//put the leftover time towards the next frame;
				currentFrame++;
				if (currentFrame >= numFrames) {
						currentFrame -= numFrames;
					}
				}
				sr.sprite = frames [currentFrame].frame;
			if (frames [currentFrame].terminal) {
				nextAnimation.Play (frames [currentFrame].toFrame);
				Reset ();
				break;
			}
			yield return null;
			}
		}
	}

	[System.Serializable]
public class Frame{
		public bool terminal = false;
		public Sprite frame;
		public Pivot[] transitions;
		public int toFrame;
	}
	[System.Serializable]
public class Pivot{
	public int fromFrame;
	public Seq toSeq;
	public int atFrame;
	}
	