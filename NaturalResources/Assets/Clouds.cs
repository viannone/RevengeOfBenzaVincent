using UnityEngine;
using System.Collections;

public class Clouds : MonoBehaviour {

	public float xAmountMin;
	public float xAmountMax;
	float xAmount;
	public float xRateMin;
	public float xRateMax;
	float xRate;
	float xPos;
	float yPos;
	int zPos;

	void Start(){
		xPos = transform.localPosition.z;
		xPos = transform.localPosition.x;
		yPos = transform.localPosition.y;

		xAmount = Random.Range (xAmountMin, xAmountMax);
		xRate = Random.Range (xRateMin, xRateMax);

	}

	// Update is called once per frame
	void Update () {
		float time = Time.timeSinceLevelLoad;
		float cos = Mathf.Cos (time * xRate);
		float amnt = xAmount * cos;
		transform.localPosition = new Vector3 (xPos - (amnt), transform.localPosition.y, zPos);

	}
}
