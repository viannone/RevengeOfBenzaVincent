using UnityEngine;
using System.Collections;

public class SceneCamera : MonoBehaviour {
	//listening to even flow
	Vector2 currentVel;
	public int cameraDistance;
	public float maxSpeed;
	public float targetTime;
	public bool cameraBob;
	public float cameraBobX;
	public float cameraBobY;
	public float cameraBobXTime;
	public float cameraBobYTime;

	//these variables are public for debugging purposes
	public Transform targetObject;


	// Use this for initialization
	void Start () {
		if (targetObject != null) {
			StartCoroutine ("MoveCamera");
		}
	}

	public IEnumerator MoveCamera(){
		Vector2 pos;
		Vector2 targetPos;
		float time;
		float theta;
		float distance;
		while (true) {
		pos = transform.position;
		targetPos = targetObject.position;

			if (cameraBob) {
				time = Time.timeSinceLevelLoad;
				theta = time / cameraBobYTime;
				distance = cameraBobY * Mathf.Sin(theta);
				targetPos.y += distance;

			 	theta = time / cameraBobXTime;
				distance = cameraBobX * Mathf.Sin(theta);
				targetPos.x += distance;
			}

		Vector2 intermediary = Vector2.SmoothDamp (pos, (Vector2) targetPos, ref currentVel, targetTime, maxSpeed);
		transform.position = new Vector3 (intermediary.x, intermediary.y, cameraDistance);
			yield return new WaitForEndOfFrame();
		}
	}

	public void SetTargetObject(Transform t){
		StopCoroutine ("MoveCamera");
		targetObject = t;
		StartCoroutine ("MoveCamera");
	}
}
