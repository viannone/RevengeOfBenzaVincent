using UnityEngine;
using System.Collections;

public class SpriteSwap : MonoBehaviour {
	public Sprite s;
	public SpriteRenderer sp;

	public void Swap(){
		sp.sprite = s;
	}

}
