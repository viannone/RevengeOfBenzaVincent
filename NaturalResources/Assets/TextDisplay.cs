using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TextDisplay : MonoBehaviour {

	public Text text;
	public void SetText(string s){
		text.text = s;
	}
}
