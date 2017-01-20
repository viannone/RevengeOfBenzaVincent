using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MessagePanel : MonoBehaviour {

	public static MessagePanel messagePanel;
	public Transform yearAdvanceButton;
	public enum STATE {DismissableMessage, OutOfMoney, Victory, OutOfResource, Headline};
	public STATE currState;

	public string outOfMoneyMessage;
	public Sprite outOfMoney;

	public Sprite victory;
	public Image image;
	public Sprite newsPaper;
	public Text text;
	public Text buttonText;

	public void Awake(){
		messagePanel = this;
		messagePanel.SetState(STATE.DismissableMessage,  PlayerPrefs.GetString("GreetingMessage"), PlayerPrefs.GetString("GreetingButton"));
	}

	public void SetImage(Sprite s){
		image.sprite = s;
	}
	public void SetButtonText(string s){
		buttonText.text = s;
	}

	public void SetText(string s){
		text.text = s;
	}

	public void Dismiss(){
		this.gameObject.SetActive (false);
	}
	public void ButtonPressed(){
		AudioMaster.audioMaster.PlayClick ();
		switch(currState){
		case STATE.OutOfMoney:
		case STATE.DismissableMessage:
		case STATE.Headline:
			Dismiss ();
			break;
		case STATE.OutOfResource:
			SceneManager.LoadScene("_Init");
			break;
		}
	}

	public void SetState(STATE state){
		SetState(state, null, null, 0);
	}
	public void SetState(STATE state, GameMaster.RESOURCES r){
		SetState (state, null, null, r);
	}
	public void SetState(STATE state, string text, string buttonText){
		SetState (state, text, buttonText, 0);
	}
	public void SetState(STATE state, string text, string buttonText, GameMaster.RESOURCES r){
		currState = state;
		this.gameObject.SetActive (true);
		yearAdvanceButton.gameObject.SetActive (false);
		switch (state) {

		case STATE.OutOfMoney:
			SetImage (outOfMoney);
			SetButtonText (outOfMoneyMessage);
			SetText (null);
			break;
		case STATE.DismissableMessage:
			SetImage (null);
			SetButtonText (buttonText);
			SetText (text);
			break;
		case STATE.Headline:
			SetImage (newsPaper);
			SetButtonText (buttonText);
			SetText ("\n" + text);
			break;
		case STATE.Victory:
			SetImage (victory);
			SetButtonText ("Your Score: " + ((int) GameMaster.gameMaster.resources[(int) GameMaster.RESOURCES.Money] + (int) GameMaster.gameMaster.resources[(int) GameMaster.RESOURCES.Energy] - (int) GameMaster.gameMaster.resources[(int) GameMaster.RESOURCES.Pollution]));
			SetText (null);
			break;
		case STATE.OutOfResource:
			SetImage (null);
			string a = "\nWhoops! \nLooks like you ran out of " + r.ToString ();
			a = a + "\n";
			switch(r){
			case GameMaster.RESOURCES.Minerals:
				a = a + "Minerals are vital for electronics, like the fancy device you're playing this on. \nHint: Get minerals from a quarry.";
				break;
			case GameMaster.RESOURCES.Animals:
				a = a + "Animals provide companionship and food for humans, along with serving other purposes. \nHint: Get animals from fishing boats.";
				break;
			case GameMaster.RESOURCES.Energy:
				a = a + "Energy is vital for playing computer games... and also modern society.\nHint: Get energy from Solar Panels, Windmills, and Oil Rigs.";
				break;
			case GameMaster.RESOURCES.Plants:
				a = a + "Plants provide food, oxygen, paper, wood, and habitats for all Earth's animals. \nHint: Get plants from the forest.";
				break;
			case GameMaster.RESOURCES.Water:
				a = a + "Thirsty? Water is necessary for all life.";
				break;
			}
			SetText (a);
			SetButtonText ("Let's try that again");
			string s = "\n";
			string p = "";
			int attempt = PlayerPrefs.GetInt ("Attempt");
			if (attempt == 0) {
				s = s + "Don't worry. It was only your first try.";
				p = "I'm up for the challenge.";
			} else if (attempt == 3) {
				s = s + "Not as easy as it looks, huh? \nDon't sweat it. You'll get it.";
				p = "Nothing will get in my way.";
			} else if (attempt == 2) {
				s = s + "Third time's a charm.";
				p = "I'll never give up.";
			} else if (attempt == 1) {
				s = s + "Don't worry. Focus on having fun.";
				p = "I don't get frustrated easily.";
			} else if (attempt == 4) {
				s = s + "I won't give up if you don't.";
				p = "Sounds like a deal.";
			} else if (attempt == 5) {
				s = s + "The master has failed more times than the student has tried.";
				p = "That was very wise, Mr. Game.";
			} else if (attempt == 6) {
				s = s + "Keep on trying. I'm rooting for you from inside your machine.";
				p = "Good to have you on my side.";
			} else if (attempt == 7) {
				s = s + "Never give up.";
				p = "I'll never let you down.";
			} else if (attempt == 8){
						s = s + "Believe that you will succeed.";
						p = "I believe.";
			} else if (attempt == 9){
						s = s + "You have a lot of patience and it will take you a long way.";
						p = "Thanks for the compliment, Mr. Game.";
			} else {
				s = s + "However many attempts it takes. I know you'll get it.";
				p = "I'll keep trying, no matter what.";
			}
			s = s + "\n\n";

			PlayerPrefs.SetString ("GreetingMessage", s);
			PlayerPrefs.SetString ("GreetingButton", p);
			break;
		}
	}
}
