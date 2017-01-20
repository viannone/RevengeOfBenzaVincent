using UnityEngine;
using System.Collections;

public class LolLoader : MonoBehaviour {
		
	public static LolLoader lolLoader;
	public bool loaded = false;
	public void Awake(){
		if (lolLoader == null){
			lolLoader = this;
			DontDestroyOnLoad (lolLoader.gameObject);
			Debug.Log ("Delete Me");
		}
	}

		public void LoadLol(){
			if (!loaded) {
				LoLSDK.LOLSDK.Init ("Natural Resources");
				Debug.Log ("Loaded");
				PlayerPrefs.SetInt ("Tutorial", 0);
				loaded = true;
			} else {
				Debug.Log ("Already Loaded");
			}
		}
}

