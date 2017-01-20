using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ConstructionSite : MonoBehaviour {
	public string nameOfObject;
	public Transform preFab;
	public int cost;
	public int amount;
	public int limitZeroForInf;
	public Sprite tappedOut;
	public GameMaster.RESOURCES resource;
	public Transform[] spots;
	int index =  0;
	[HideInInspector]
	public Button button;
	public Text t;
	string renewable;
	public SpriteSwap spriteSwap;
	bool swapped = false;
	public void Start(){
		button = GetComponentInChildren<Button> ();
		t = button.GetComponentInChildren<Text> ();

		if (limitZeroForInf == 0) {
			renewable = "RENEWABLE";
			t.color = Color.black;
		} else if (limitZeroForInf == -1) {
			renewable = "INEXHAUSTABLE";
			t.color = Color.grey;
		}else {
			renewable = "NON-RENEWABLE";
			t.color = Color.red;
		}
		UpdateText ();
	}

	public void UpdateText(){
		int remaining = spots.Length - index;
		t.text = "Create " + nameOfObject + "\nCost: $" + cost + "\n+" + amount.ToString() + " " + resource.ToString() + " per year" + "\n" + renewable + "\nRemaining: " + remaining;
	}

	public void Construct(){
		AudioMaster.audioMaster.PlayClick ();
		if (GameMaster.gameMaster.resources[(int) GameMaster.RESOURCES.Money] - cost >= 0) {
			GameMaster.gameMaster.AddResource (GameMaster.RESOURCES.Money, -cost);
			GameMaster.gameMaster.productions [(int)resource] += amount;
			GameMaster.gameMaster.AddResource (resource, 0);
			Transform t = (Transform)Instantiate (preFab, spots[index].position, Quaternion.identity);
			Tickable tick = t.GetComponent<Tickable> ();
			tick.SetAmount (amount);
			tick.SetLimit (limitZeroForInf);
			tick.tappedOut = tappedOut;
			tick.SetResource (resource);
			index++;
			UpdateText ();
			if (index == spots.Length) {
				this.gameObject.SetActive (false);
			}
			if (spriteSwap != null && swapped == false) {
				swapped = true;
				spriteSwap.Swap ();
			}
		} else {
			Debug.Log (MessagePanel.messagePanel);
			MessagePanel.messagePanel.SetState (MessagePanel.STATE.OutOfMoney);
		}
	}

}
