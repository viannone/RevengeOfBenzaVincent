using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
public class GameMaster : MonoBehaviour {

	public static GameMaster gameMaster;
	public GameObject dispPrefab;
	public Transform canvas;
	public Transform moneyPollCanvas;
	public enum RESOURCES {Water, Plants, Animals, Minerals, Oil, Electricity, Energy, Money, Pollution}; 
	public Text[] resourceCounter;
	public List<Tickable> tickables;
	public Transform tutorialBen;
	public Text yearAdvanceButton;
	public Text blimpText;
	public Slider slider;
	public int currentYear = 1950;
	public int maxYear = 2050;
	public int[] resources = new int[RESOURCES.GetNames (typeof(RESOURCES)).Length];
	public int[] productions;
	public int[] consumptions;
	public int attempt;

	public enum GameEndState {TimeUp, OutOfResources};
	void Awake (){
		if (PlayerPrefs.GetInt ("Attempt") == 0) {
			PlayerPrefs.SetString ("GreetingMessage", "Good luck on your first run through. It may be challenging, but don't get discouraged.");
			PlayerPrefs.SetString ("GreetingButton", "I'm ready for anything.");
		}
		attempt = PlayerPrefs.GetInt("Attempt");
		resourceCounter = new Text[RESOURCES.GetNames (typeof(RESOURCES)).Length];
		if (gameMaster == null) {
			gameMaster = this;
		}
		tickables = new List<Tickable>();
		consumptions = new int[System.Enum.GetNames (typeof(RESOURCES)).Length];
		foreach (int i in consumptions) {
			consumptions[i] = 0;
		}
		productions = new int[System.Enum.GetNames (typeof(RESOURCES)).Length];
		foreach (int i in productions) {
			productions[i] = 0;
		}
	}
	void Start () {
		if (PlayerPrefs.GetInt ("Attempt") == 0) {
			StartTutorial ();
		} else {
			Tutorial.tutorial.End ();
		}
		Debug.Log ("Attempt: " + PlayerPrefs.GetInt ("Attempt"));
		PlayerPrefs.SetInt("Attempt", PlayerPrefs.GetInt ("Attempt") + 1);
		slider.maxValue = maxYear;
		slider.minValue = currentYear;
		slider.value = currentYear;
		for (int i = 0; i < resourceCounter.Length - 2; i++) {
			GameObject a = (GameObject)Instantiate (dispPrefab, new Vector3 (0, 0, 0), Quaternion.identity, canvas);
			resourceCounter [i] = a.GetComponentInChildren<Text> ();
		}
		for (int i = resourceCounter.Length - 2; i < resourceCounter.Length; i++) {
			GameObject a = (GameObject)Instantiate (dispPrefab, new Vector3 (0, 0, 0), Quaternion.identity, moneyPollCanvas);
			resourceCounter [i] = a.GetComponentInChildren<Text> ();
		}
		canvas.GetComponent<DisplayButtons> ().AddButtons ();
		moneyPollCanvas.GetComponent<DisplayButtons> ().AddButtons ();

		UpdateYear ();
		InitTexts ();
		LolLoader.lolLoader.LoadLol ();
	}

	public void Tick(){
		City.city.Consume ();
		foreach (Tickable t in tickables) {
			t.Tick ();
		}

		currentYear++;
		UpdateYear ();
		if (currentYear == 2100) {
			MessagePanel.messagePanel.SetState (MessagePanel.STATE.Victory);
		}

		if (currentYear == 2050) {
			MessagePanel.messagePanel.SetState (MessagePanel.STATE.Headline, "EXTRA EXTRA: \n\nCold Fusion Discovered. \n You may no longer sell energy to the city.", "Gee, energy independance is expensive.");
			City.city.energyConsumption = 0;
			City.city.energyPrice = 0;
			City.city.UpdateConsumptions ();
		}
		if (currentYear == 1965) {
			MessagePanel.messagePanel.SetState (MessagePanel.STATE.Headline, "EXTRA EXTRA: \n\nPopulation Boom!\nAnimal and plant consumption doubled!!!", "Sweet. Thanks!");
			City.city.foodConsumption = City.city.foodConsumption * 2;
			City.city.UpdateConsumptions();
		}
		if (currentYear == 1990) {
			MessagePanel.messagePanel.SetState (MessagePanel.STATE.Headline, "EXTRA EXTRA: \n\nEngagement Rate Way Up!\nMinerals now worth triple!!!", "Really?? Alright!!");
			City.city.mineralPrice = City.city.mineralPrice * 3;
			City.city.UpdateConsumptions();
		}
		if (currentYear == 1995) {
			MessagePanel.messagePanel.SetState (MessagePanel.STATE.Headline, "EXTRA EXTRA: \n\nNew Study Finds Having Kids Ruins Finances!\nAnimal And Plant Prices Doubled", "Wow!");
			City.city.foodPrice = City.city.foodPrice * 2;
			City.city.UpdateConsumptions();
		}
		if (currentYear == 2000) {
			MessagePanel.messagePanel.SetState (MessagePanel.STATE.Headline, "EXTRA EXTRA: \n\nNew Game Console: Tin-endo Pee\nEnergy consumption doubled!!!", "My thumbs are sore just thinking about it");
			City.city.energyConsumption = City.city.energyConsumption * 2;
			City.city.UpdateConsumptions ();
		}
		if (currentYear == 2010) {
			MessagePanel.messagePanel.SetState (MessagePanel.STATE.Headline, "EXTRA EXTRA: \n\nGovernor buys another SUV \n Oil prices doubled!!!", "Yesssssss, more money!");
			City.city.oilPrice = City.city.oilPrice * 2;
			City.city.UpdateConsumptions ();
		}
		if (currentYear == 2016) {
			MessagePanel.messagePanel.SetState (MessagePanel.STATE.Headline, "EXTRA EXTRA: \n\nPresident says videogames good for health\n Energy consumption doubled!!!", "Woohoo!!!");
			City.city.energyConsumption = City.city.energyConsumption * 2;
			City.city.UpdateConsumptions ();
		}
		if (currentYear == 2020) {
			MessagePanel.messagePanel.SetState (MessagePanel.STATE.Headline, "EXTRA EXTRA: \n\nPresident caught picking nose\n Worldwide Panic\nOil Prices Doubled", "What???");
			City.city.oilPrice = City.city.oilPrice * 2;
			City.city.UpdateConsumptions ();
		}
		if (currentYear == 2021) {
			MessagePanel.messagePanel.SetState (MessagePanel.STATE.Headline, "EXTRA EXTRA: \n\nPresident Empeached Over Nose Picking Scandal\nNew President Sleeps With Nightlight\nEnergy Rates Doubled", "...Okay");
			City.city.energyPrice = City.city.energyPrice * 2;
			City.city.UpdateConsumptions ();
		}
		if (currentYear == 2030) {
			MessagePanel.messagePanel.SetState (MessagePanel.STATE.Headline, "EXTRA EXTRA: \n\nPresident Quits, Leaves On Rocketship to Mars\nNew President To Be Elected Shortly", "This is Ridiculous");
		}
		if (currentYear == 2032) {
			MessagePanel.messagePanel.SetState (MessagePanel.STATE.Headline, "EXTRA EXTRA: \n\nPresident Arrested On Mars, Robot Elected As Replacement\nEnergy Prices Doubled", "I really should've voted");
			City.city.energyPrice = City.city.energyPrice * 2;
			City.city.UpdateConsumptions ();
		}
		if (currentYear == 2040) {
			MessagePanel.messagePanel.SetState (MessagePanel.STATE.Headline, "EXTRA EXTRA: \n\nRobot Opens Robotics Factories Across Country\nEnergy Consumption Doubled", "Oh Wow!");
			City.city.energyConsumption = City.city.energyConsumption * 2;
			City.city.UpdateConsumptions ();
		}
		if (currentYear == maxYear) {
			MessagePanel.messagePanel.SetState (MessagePanel.STATE.Victory);
		}

		for (int i = 0; i < resources.Length; i++) {
			if (resources [i] < 0) {
				MessagePanel.messagePanel.SetState (MessagePanel.STATE.OutOfResource, (GameMaster.RESOURCES)i);
			}
		}
	}

	public void EndGame(){
		LoLSDK
	}

	public void UpdateYear(){
		yearAdvanceButton.text = "Current Year: " + currentYear + "\n Go To Next Year";
		if (blimpText != null) {
			blimpText.text = currentYear.ToString();
		}
		slider.value = currentYear;
	}

	public void AddToList(Tickable t){
		tickables.Add (t);
	}
	public void AddResource(RESOURCES r, int amount){
		int i = (int) r;
		resources [i] += amount;
		if (productions [i] > consumptions [i]) {
			resourceCounter [i].color = new Color (.1f, .3f, 0);
		} else if (productions [i] < consumptions [i]) {
			resourceCounter [i].color = new Color (1.0f, .5f, 0);
		} else {
			resourceCounter [i].color = Color.black;
		}
		if (resources[i] <= 5 && productions[i] < consumptions[i]){
			resourceCounter [i].color = Color.red;
		}
		if (r == RESOURCES.Pollution){
			resourceCounter[i].color = Color.gray;
		}
		if (r == RESOURCES.Oil || r == RESOURCES.Electricity) {
			resourceCounter [i].text = r.ToString () + " (To Energy)\n+" + productions [i] + "/-" + consumptions [i] + " per year";
		} else if (r == RESOURCES.Money || r == RESOURCES.Pollution) {
			resourceCounter [i].text = r.ToString () + ": " + resources [(int)r] + "\n+" + productions [i] + " per year";
		}else{
			resourceCounter [i].text = r.ToString () + ": " + resources [(int)r] + "\n+" + productions [i] + "/-" + consumptions [i] + " per year";
		}


	}

	public void InitTexts(){
		for (int i = 0; i < (int) System.Enum.GetNames(typeof(RESOURCES)).Length; i++){
			AddResource( (RESOURCES) i, 0);
		}
	}
	public void StartTutorial(){
		Tutorial.tutorial.Begin ();
	}
	public void ResetAttempts(){
		PlayerPrefs.SetInt ("Attempt", 0);
		PlayerPrefs.SetInt ("Tutorial", 0);
	}
}
	