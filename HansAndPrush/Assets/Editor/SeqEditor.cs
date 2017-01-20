using UnityEngine;
using System.Collections;
using UnityEditor;
[CustomEditor (typeof(Seq))]
public class SeqEditor : Editor {
	public override void OnInspectorGUI() {
		DrawDefaultInspector ();

		Seq seq = (Seq)target;
		if(GUILayout.Button("Compile Transitions")){
			seq.CompileTransitions ();
		}
		if (GUILayout.Button ("Find Frames")) {
			seq.GetFrames ();
		}
	}
}
