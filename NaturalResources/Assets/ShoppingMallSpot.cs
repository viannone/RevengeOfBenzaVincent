using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShoppingMallSpot : MonoBehaviour{
	public int cost;
	public string shop;
	public string display;
	public Button button;
	public Transform nextButton;
	public Transform prefab;
	public Transform spot;
	public GameMaster.RESOURCES r;
	Text text;

	public void Start(){
		text = button.GetComponentInChildren<Text> ();
		text.text = "Create " + shop + "\nCost: $" + cost + "\nIncreases " + r.ToString() + " Consumption\nby 1 per year";
	}
	public void ButtonClicked(){
		AudioMaster.audioMaster.PlayClick ();
		if (GameMaster.gameMaster.resources [(int)GameMaster.RESOURCES.Money] >= cost) {
			if (nextButton != null) {
				nextButton.gameObject.SetActive (true);
			}
			this.gameObject.SetActive (false);
			GameMaster.gameMaster.consumptions [(int) r] += 1;

				GameMaster.gameMaster.AddResource (GameMaster.RESOURCES.Money, -cost);
				if (prefab != null) {
				Transform a = (Transform) Instantiate (prefab, spot.position, Quaternion.identity);
				a.GetComponent<TextDisplay>().SetText (display);
				} else {
					Debug.LogError ("ShoppingMallPrefabNotAssigned");
				}
		} else {
			MessagePanel.messagePanel.SetState (MessagePanel.STATE.OutOfMoney);
		}
	}
}