using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {


	public Button[] buttonsToDisable;
	public Button tutorialButton;
	public Text tutorialButtonText;
	public RectTransform ben;
	public Image benSprite;
	public Sprite benNormal;
	public Sprite benPointingUp;
	public Sprite benPointingDown;
	public GameObject skipTutorial;
	public Transform benSpeech;
	public Transform backGround;
	public Text benSpeechText;
	public int index = 0;
	public static Tutorial tutorial;
	public Tut[] tutorialSpots;

	void Awake(){
		if (tutorial == null){
			tutorial = this;
		}
		benSprite = ben.transform.GetComponent<Image> ();
		benSpeechText = benSpeech.GetComponent<Text> ();
	}
	public void Begin(){
		if (PlayerPrefs.GetInt ("Tutorial") != 0) {
			skipTutorial.SetActive (true);
		}
		PlayerPrefs.SetInt ("Tutorial", 1);
		ReloadInterface ();
		foreach (Button b in buttonsToDisable) {
			b.interactable = false;
		}
		Iterate (0);
		Debug.Log ("GotCalled");
	}

	public void End(){
		tutorialButton.gameObject.SetActive (false);
		ben.gameObject.SetActive(false);
		benSpeech.gameObject.SetActive(false);
		backGround.gameObject.SetActive (false);
		foreach (Button b in buttonsToDisable) {
			b.interactable = true;
		}
	}

	public void ReloadInterface(){
		tutorialButton.gameObject.SetActive (true);
		ben.gameObject.SetActive(true);
		benSpeech.gameObject.SetActive(true);
		backGround.gameObject.SetActive (true);
		index = 0;
		foreach (Button b in buttonsToDisable) {
			b.interactable = false;
		}
	}

	public void Iterate(int i){
		Tut newTut = tutorialSpots [i];
		if (newTut.updatePos) {
			ben.anchoredPosition = newTut.benPosition;
		}
		benSprite.sprite = newTut.ben;
		benSpeechText.text = newTut.text;
		tutorialButtonText.text = newTut.buttonText;
		if (newTut.go != null) {
			newTut.go.onClick.Invoke ();
		}
		index++;
	}

	public void ButtonPressed(){
		AudioMaster.audioMaster.PlayClick ();
		if (index < tutorialSpots.Length) {
			Iterate (index);
		} else {
			End ();
		}
	}

	public void Back(){
		AudioMaster.audioMaster.PlayClick ();
		if (index - 2 >= 0) {
			index = index - 2;
			Iterate (index);
		}
	}

	[System.Serializable]
	public class Tut{
		public Vector2 benPosition;
		public Sprite ben;
		public string text;
		public string buttonText;
		public bool flipX;
		public Button go;
		public bool updatePos;
	}
}
