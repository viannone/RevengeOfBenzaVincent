using UnityEngine;
using System.Collections;

public class LocationsButton : MonoBehaviour {
	CameraScript cameraScript;
	AudioMaster audioMaster;
	public AudioClip soundEffect;
	public AudioClip envSound;
	public int cameraMove;
	public void Start(){
		cameraScript = CameraScript.cameraScript;
		audioMaster = AudioMaster.audioMaster;
	}

	public void Click(){
		cameraScript.moveCamera (cameraMove);
		if (soundEffect != null) {
			audioMaster.PlaySoundEffect (soundEffect);
		}
		audioMaster.ChangeEnvironmentSound (envSound);
		audioMaster.PlayClick ();
	}
		
}
