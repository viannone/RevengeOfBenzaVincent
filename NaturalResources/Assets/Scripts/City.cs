using UnityEngine;
using System.Collections;

public class City : MonoBehaviour {

	public int waterPrice = 5;
	public int oilPrice = 10;
	public int gasPrice = 5;
	public int electrictyPrice = 1;
	public int energyPrice = 3;
	public int foodPrice = 2;
	public int soilPrice = 1;
	public int mineralPrice = 4;

	public int oilConsumption = 1;
	public int energyConsumption = 1;
	public int foodConsumption = 1;
	public int gasConsumption = 1;
	public int waterConsumption = 1;
	public int soilConsumption = 1;
	public int mineralConsumption = 1;
	public int electricityConsumption = 1;

	public static City city;
	GameMaster g;
	public void Awake(){
		if (city == null) {
			city = this;
		}
	}
	public void Start(){
		g = GameMaster.gameMaster;
		UpdateConsumptions ();
	}

	public void UpdateConsumptions(){
		oilConsumption = g.productions [(int)GameMaster.RESOURCES.Oil];
		electricityConsumption = g.productions [(int) GameMaster.RESOURCES.Electricity];
		g.consumptions[(int) GameMaster.RESOURCES.Oil] = oilConsumption;
		g.consumptions [(int)GameMaster.RESOURCES.Water] = waterConsumption;
		g.consumptions [(int)GameMaster.RESOURCES.Minerals] = mineralConsumption;
		g.consumptions[(int) GameMaster.RESOURCES.Energy] = energyConsumption;
		g.consumptions[(int) GameMaster.RESOURCES.Animals] = foodConsumption;
		g.consumptions[(int) GameMaster.RESOURCES.Plants] = foodConsumption;
		g.consumptions [(int)GameMaster.RESOURCES.Electricity] = electricityConsumption;
		g.productions[(int) GameMaster.RESOURCES.Energy] = gasConsumption + oilConsumption + electricityConsumption;
		g.productions [(int) GameMaster.RESOURCES.Pollution] = ((gasConsumption + oilConsumption) * 10);
		g.productions [(int) GameMaster.RESOURCES.Money] = (foodConsumption * (2 * foodPrice)) + (energyConsumption * energyPrice) + (gasConsumption * gasPrice) + (oilConsumption + oilPrice) + (waterConsumption + waterPrice) + (soilConsumption + soilPrice) + (mineralConsumption + mineralPrice) ;
	}

	public void Convert(){

	}

	public void Consume(){
		g.AddResource (GameMaster.RESOURCES.Animals, -foodConsumption);
		g.AddResource (GameMaster.RESOURCES.Plants, -foodConsumption);
		g.AddResource (GameMaster.RESOURCES.Energy, (gasConsumption + oilConsumption + electricityConsumption));
		g.AddResource (GameMaster.RESOURCES.Energy, -energyConsumption);
		g.AddResource (GameMaster.RESOURCES.Oil, -oilConsumption);
		g.AddResource (GameMaster.RESOURCES.Water, -waterConsumption);
		g.AddResource (GameMaster.RESOURCES.Minerals, -mineralConsumption);
		g.AddResource (GameMaster.RESOURCES.Pollution, (gasConsumption + oilConsumption) * 10);
		g.AddResource (GameMaster.RESOURCES.Money, (foodConsumption * (2 * foodPrice)) + (energyConsumption * energyPrice) + (gasConsumption * gasPrice) + (oilConsumption * oilPrice) + (waterConsumption * waterPrice) + (soilConsumption * soilPrice) + (mineralConsumption * mineralPrice));
			}
		}


