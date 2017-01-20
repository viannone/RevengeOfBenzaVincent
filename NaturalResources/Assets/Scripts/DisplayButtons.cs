using UnityEngine;
using System.Collections;

public class DisplayButtons : MonoBehaviour {
	RectTransform rect;
	Transform[] textDisplays;
	Canvas moneyPollCanvas;
	// Use this for initialization
	void Awake(){
		rect = gameObject.GetComponent<RectTransform> ();
	}
	public void AddButtons(){
		int c = transform.childCount;
		textDisplays = new Transform[c];
		for (int i = 0; i < c; i++) {
			textDisplays [i] = transform.GetChild (i);
		}
		UpdateLayout ();
	}
	public void UpdateLayout(){
		int w = (int) rect.rect.width;
		int h = (int) rect.rect.height;
		int c = (int) textDisplays.Length /2;
		int d = textDisplays.Length - c;
		int hOffset = (int) h / 12;
		for (int i = 0; i < c; i++) {
			//textDisplays [i].localPosition = new Vector2 (i * (w/c), h - hOffset);
			RectTransform button = textDisplays[i].GetComponent<RectTransform> ();
			button.anchorMin = new Vector2 ((float) i/c, .5f);
			button.anchorMax = new Vector2 ((float) (i+1)/c,1);
			button.offsetMax = new Vector2 (0, 0);
			button.offsetMin = new Vector2 (0, 0);
		}
		for (int i = 0; i < d; i++) {
			//textDisplays [i+c].localPosition = new Vector2 (i * (w/d), 0);
			RectTransform button = textDisplays[i + c].GetComponent<RectTransform> ();
			button.anchorMin = new Vector2 ((float) i/d, 0.0f);
			button.anchorMax = new Vector2 ((float) (i+1)/d, .5f);
			button.offsetMax = new Vector2 (0, 0);
			button.offsetMin = new Vector2 (0, 0);
		}
	}
}
