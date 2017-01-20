using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class CameraScript : MonoBehaviour {

	public static CameraScript cameraScript;

	public float volume = 1.0f;
	public void Awake(){
			cameraScript = this;
	}
	public void Start(){
		moveCamera (80);
	}

	public void moveCamera(int x){
		transform.position = new Vector3(x, 0, -10);
	}


}
