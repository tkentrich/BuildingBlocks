using UnityEngine;
using System.Collections;

public class LevelResult : MonoBehaviour {

	int attempts;
	int steps;
	int coinsCollected;
	int diamondsCollected;
	int coinsTotal;
	int diamondsTotal;

	public void Reset () {
		attempts++;
		steps = 0;
		coinsCollected = 0;
		diamondsCollected = 0;
	}

	public void AddCoin () {
		coinsTotal++;
	}
	public void AddDiamond () {
		diamondsTotal++;
	}

	public void SetTotals (int coins, int diamonds) {
		coinsTotal = coins;
		diamondsTotal = diamonds;
	}
	public void Step() {
		steps++;
	}

	public void Collect (Collectible c) {
		if (c.type == PlayerInfo.CollectibleType.Coin) {
			CollectCoins(c.quantity);
		} else if (c.type == PlayerInfo.CollectibleType.Diamond) {
			CollectDiamonds(c.quantity);
		}
	}

	public void CollectCoins (int amount) {
		coinsCollected += amount;
	}

	public void CollectDiamonds (int amount) {
		diamondsCollected += amount;
	}

	public int CoinsCollected () {
		return coinsCollected;
	}
	public int CoinsTotal () {
		return coinsTotal;
	}

	public int DiamondsCollected () {
		return diamondsCollected;
	}
	public int DiamondsTotal () {
		return diamondsTotal;
	}

	public int Attempts() {
		return attempts;
	}
	public int Steps() {
		return steps;
	}

	public LevelResult empty() {
		LevelResult toReturn = new LevelResult();
		toReturn.SetTotals(coinsTotal, diamondsTotal);
		for (int i = 0; i < 100; i++) {
			toReturn.Reset();
		}
		for (int i = 0; i < 10000; i++) {
			toReturn.Step();
		}
		return toReturn;
	}
}
