using UnityEngine;
using System.Collections;

public class LevelResult : MonoBehaviour {

	int attempts;

	int coinsCollected;
	int diamondsCollected;
	int coinsTotal;
	int diamondsTotal;

	public void Reset () {
		attempts++;
		coinsCollected = 0;
		diamondsCollected = 0;
	}

	public void SetTotals (int coins, int diamonds) {
		coinsTotal = coins;
		diamondsTotal = diamonds;
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
}
