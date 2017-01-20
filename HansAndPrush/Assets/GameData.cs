using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.IO;

public class GameData : MonoBehaviour {
	public static GameData gameData;
	public static List<Transform> enemies;

	public bool blueUnlocked;
	public bool redUnlocked;
	public bool greenUnlocked;
	public bool purpleUnlocked;
	public bool whiteUnlocked;
	public bool blackUnlocked;
	public bool orangeUnlocked;
	public bool yellowUnlocked;

	public int currentLevel;

	public void OnAwake(){
		if (gameData == null) {
			gameData = this;
		} else if (gameData != this) {
			Destroy (gameObject);
		}
		DontDestroyOnLoad (this);
		enemies = new List<Transform> ();
	}

	public void Save(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/playerData.Prush");
		bf.Serialize (file, PlayerData.Pack ());
		file.Close ();
	}
	public void Load(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/playerData.Prush");
		PlayerData data = (PlayerData)bf.Deserialize (file);
		file.Close ();
		data.Unpack ();
	}

}

[Serializable]
class PlayerData{
	public bool blueUnlocked;
	public bool redUnlocked;
	public bool greenUnlocked;
	public bool purpleUnlocked;
	public bool whiteUnlocked;
	public bool blackUnlocked;
	public bool orangeUnlocked;
	public bool yellowUnlocked;

	public int currentLevel;

	public static PlayerData Pack(){
		GameData data = GameData.gameData;
		PlayerData playerData = new PlayerData ();
		playerData.blueUnlocked = data.blueUnlocked;
		playerData.redUnlocked = data.redUnlocked;
		playerData.greenUnlocked = data.greenUnlocked;
		playerData.purpleUnlocked = data.purpleUnlocked;
		playerData.whiteUnlocked = data.whiteUnlocked;
		playerData.blackUnlocked = data.blackUnlocked;
		playerData.orangeUnlocked = data.orangeUnlocked;
		playerData.yellowUnlocked = data.yellowUnlocked;
		playerData.currentLevel = data.currentLevel;
		return playerData;
	}
	private PlayerData(){
	}
	public void Unpack(){
		GameData data = GameData.gameData;
		data.blueUnlocked = blueUnlocked;
		data.redUnlocked = redUnlocked;
		data.greenUnlocked = greenUnlocked;
		data.purpleUnlocked = purpleUnlocked;
		data.whiteUnlocked = whiteUnlocked;
		data.blackUnlocked = blackUnlocked;
		data.orangeUnlocked = orangeUnlocked; 
		data.yellowUnlocked = yellowUnlocked;
		data.currentLevel = currentLevel; 
	}
}
