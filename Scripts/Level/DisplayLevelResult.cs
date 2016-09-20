using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplayLevelResult : MonoBehaviour {

	public Text levelName;
	public Text coins;
	public Text diamonds;
	public Text steps;
	public Text attempts;
	public Text bestCoins;
	public Text bestDiamonds;
	public Text bestSteps;
	public Text bestAttempts;
	public Text newRecord;

	public LevelResult result;
	public LevelResult bestResult;
	private bool active = false;

	public void Display(LevelResult result) {
		GameProgress prog = FindObjectOfType<GameProgress>();
		if (prog == null) {
			return;
		}

		bool beatBest = false;
		bestResult = prog.GetCurrentResult();

		if (bestResult == null) {
			bestResult = result.empty();
			beatBest = true;
		} else if (bestResult.DiamondsCollected() < result.DiamondsCollected()) {
			beatBest = true;
		} else if (bestResult.DiamondsCollected() == result.DiamondsCollected()) {
			if (bestResult.CoinsCollected() < result.CoinsCollected()) {
				beatBest = true;
			} else if (bestResult.CoinsCollected() == result.CoinsCollected()) {
				if (bestResult.Attempts() > result.Attempts()) {
					beatBest = true;
				} else if (bestResult.Attempts() == result.Attempts()) {
					if (bestResult.Steps() > result.Steps()) {
						beatBest = true;
					}
				}
			}
		}

		levelName.text = prog.LevelName();
		coins.text = "" + result.CoinsCollected() + " / " + result.CoinsTotal();
		diamonds.text = "" + result.DiamondsCollected() + " / " + result.DiamondsTotal();
		attempts.text = "" + result.Attempts();
		steps.text = "" + result.Steps();

		bestCoins.text = "" + bestResult.CoinsCollected() + " / " + bestResult.CoinsTotal();
		bestDiamonds.text = "" + bestResult.DiamondsCollected() + " / " + bestResult.DiamondsTotal();
		bestAttempts.text = "" + bestResult.Attempts();
		bestSteps.text = "" + bestResult.Steps();

		if (beatBest) {
			prog.SetCurrentResult(result);
			prog.AdjustTotalCoins(result.CoinsCollected() - bestResult.CoinsCollected());
			prog.AdjustTotalDiamonds(result.DiamondsCollected() - bestResult.DiamondsCollected());
			newRecord.gameObject.SetActive(true);
		}

		gameObject.SetActive(true);

	}
}
