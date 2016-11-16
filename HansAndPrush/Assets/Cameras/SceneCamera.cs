using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SceneCamera : MonoBehaviour {
	//listening to even flow
	Transform hans;
	Vector2 currentVel;
	public int cameraDistance;
	public float maxSpeed;
	public float targetTime;
	public bool cameraBob;
	public float cameraBobX;
	public float cameraBobY;
	public float cameraBobXTime;
	public float cameraBobYTime;
	public int pauseInput = 0;
	public bool paused = false;
	public bool pauseButtonDown = false;
	public float timeScale = 0.0f;
	public Image pauseScreen;
	//these variables are public for debugging purposes
	public Transform targetObject;


	// Use this for initialization
	void Start () {
		hans = GameObject.FindGameObjectWithTag ("Player").transform;
		if (targetObject != null) {
			StartCoroutine ("MoveCamera");
		}
		StartCoroutine ("PauseManage");
	}
	public IEnumerator PauseManage(){
		while (true) {
			pauseInput = (int) Input.GetAxisRaw ("Pause");
			if (pauseInput > 0 && pauseButtonDown == false) {
				TogglePause ();
				pauseButtonDown = true;
			}
			if (pauseInput == 0) {
				pauseButtonDown = false;
			}
			yield return new WaitForSecondsRealtime (0.1f);
		}
	}

	public void TogglePause(){
		paused = !paused;
		if (paused) {
			timeScale = Time.timeScale;
			ShowPauseScreen ();
			Time.timeScale = 0.0f;
		} else {
			HidePauseScreen ();
			Time.timeScale = timeScale;
		}
	}

	public void ShowPauseScreen(){
		pauseScreen.gameObject.SetActive (true);
	}
	public void HidePauseScreen(){
		pauseScreen.gameObject.SetActive (false);
	}

	public IEnumerator MoveCamera(){
		Vector2 pos;
		Vector2 targetPos;
		float time;
		float theta;
		float distance;
		while (true) {
			try {
				pos = transform.position;
				targetPos = targetObject.position;

				if (cameraBob) {
					time = Time.timeSinceLevelLoad;
					theta = time / cameraBobYTime;
					distance = cameraBobY * Mathf.Sin (theta);
					targetPos.y += distance;

					theta = time / cameraBobXTime;
					distance = cameraBobX * Mathf.Sin (theta);
					targetPos.x += distance;
				}

				Vector2 intermediary = Vector2.SmoothDamp (pos, (Vector2)targetPos, ref currentVel, targetTime, maxSpeed);
				transform.position = new Vector3 (intermediary.x, intermediary.y, cameraDistance);
			} catch (MissingReferenceException) {
				SetTargetObject (hans);
			}
			yield return new WaitForEndOfFrame ();
		}
	}
	public void SetTargetObject(Transform t){
		StopCoroutine ("MoveCamera");
		targetObject = t;
		StartCoroutine ("MoveCamera");
	}
}
