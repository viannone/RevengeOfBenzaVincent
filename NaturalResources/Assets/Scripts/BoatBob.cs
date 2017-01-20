using UnityEngine;
using System.Collections;

public class BoatBob : MonoBehaviour {
	public bool yBob = true;
	public float yAmount = 0;
	public float yRate = 0;
	public bool xBob = true;
	public float xAmount = 0;
	public float xRate = 0;
	public bool rotate = true;
	public float rAmount = 0;
	public float rRate = 0;
	public int zPos = 0;
	SpriteRenderer sr;
	public Sprite sprite;
	public Sprite altSprite;
	public bool spriteSwap;
	bool initialXFlip;
	public bool xFlip = false;

	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer> ();
		StartCoroutine (Bob ());
		initialXFlip = sr.flipX;
	}

	public IEnumerator Bob(){
		float yPos = transform.localPosition.y;
		float xPos = transform.localPosition.x;
		float rrr;
		float time;
		float oldCos = 0;
		while (true) {
			time = Time.timeSinceLevelLoad;

			if (yBob) {
				transform.localPosition = new Vector3 (xPos, yPos + (yAmount * (Mathf.Sin (time * yRate))), zPos);
			}
			if (xBob) {
				float cos = Mathf.Cos (time * xRate);
				float amnt = xAmount * cos;
				transform.localPosition = new Vector3 (xPos - (amnt), transform.localPosition.y, zPos);
				if (xFlip) {
					if (oldCos < cos) {
						sr.flipX = (!initialXFlip);
					} else {
						sr.flipX = initialXFlip;
					}
					oldCos = cos;
				}
				if (spriteSwap) {
					if (oldCos < cos) {
						sr.sprite = altSprite;
					} else {
						sr.sprite = sprite;
					}
					oldCos = cos;
				}
			}
			if (rotate) {
				rrr = rAmount * (Mathf.Sin (time * rRate));
				transform.RotateAround (transform.localPosition, Vector3.forward, rrr);
			}
			yield return null;
		}
	}
}
