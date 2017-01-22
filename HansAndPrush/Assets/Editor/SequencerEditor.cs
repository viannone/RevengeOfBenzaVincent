using UnityEngine;
using System.Collections;
using UnityEditor;
[CustomEditor (typeof(Sequencer))]
public class SequencerEditor : Editor {
	public override void OnInspectorGUI() {
		DrawDefaultInspector ();

		Sequencer sequencer = (Sequencer)target;
		if(GUILayout.Button("Map Sequences")){
			sequencer.MapSequences ();
		}
	}
}
