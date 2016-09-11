using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerInfo : MonoBehaviour {

	public enum AbilityType {Lift, Climb, ClimbWBlock};
	public enum CollectibleType {Diamond, Coin};

	public Text inventoryText;

	ArrayList abilities;
	Hashtable inventory;

	void Awake() {
		abilities = new ArrayList();
		inventory = new Hashtable();
		inventory.Add(CollectibleType.Diamond, 0);
		inventory.Add(CollectibleType.Coin, 0);
		SetText();
	}

	public void Collect(Collectible c) {
		CollectibleType type;
		int quantity;
		c.Collect(out type, out quantity);
		inventory[type] = (int)inventory[type] + quantity;
		SetText();
	}

	public void GiveAbility(AbilityType ability) {
		if (!abilities.Contains(ability)) {
			abilities.Add(ability);
		}
	}

	public void RemoveAbility(AbilityType ability) {
		if (abilities.Contains(ability)) {
			abilities.Remove(ability);
		}
	}

	public bool HasAbility(AbilityType ability) {
		return (abilities.Contains(ability));
	}

	public string InventoryString() {
		string toReturn = ""; // "INVENTORY\n\n";
		int diamonds = (int)inventory[CollectibleType.Diamond];
		int coins = (int)inventory[CollectibleType.Coin];
		toReturn += "Coins: " + coins + "\n";
		toReturn += "Diamonds: " + diamonds + "\n";
		return toReturn;
	}

	void Update() {
		SetText();
	}

	public void SetText() {
		if (inventoryText != null) {
			inventoryText.text = InventoryString();
		}
	}
}
