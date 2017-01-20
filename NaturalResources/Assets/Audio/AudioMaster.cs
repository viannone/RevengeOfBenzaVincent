using UnityEngine;
using System.Collections;

public class AudioMaster : MonoBehaviour {
	public static AudioMaster audioMaster;

	public void Awake(){
		audioMaster = this;
	}
	public AudioClip click;

	public AudioSource music;
	public AudioSource environment;
	public AudioSource soundEffects;
	public void ChangeMusicVolume(float f){
		music.volume = f;
	}
	public void ChangeEnvironmentVolume(float f){
		environment.volume = f;
	}
	public void ChangeSFXvolume(float f){
		soundEffects.volume = f;
	}
	public void ChangeEnvironmentSound(AudioClip audio){
		environment.clip = audio;
		environment.Play ();
	}
	public void PlaySoundEffect(AudioClip audio){
		soundEffects.clip = audio;
		soundEffects.Play();
	}
	public void PlayClick(){
		PlaySoundEffect (click);
	}
}
