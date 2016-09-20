using UnityEngine;
using UnityEngine.SceneManagement;

using System.Collections;

public class GameProgress : MonoBehaviour {

	private int coins = 0;
	private int diamonds = 0;

	public bool inInitializer;
	public int levelIndex = 0;

	private int[] willUnlock;

	ArrayList unlocked;
	Hashtable results;

	void Start () {
		unlocked = new ArrayList();
		results = new Hashtable();
		LevelData.InitData();
		foreach (int i in LevelData.InitialUnlock()) {
			Unlock(i);
		}
		if (inInitializer) {
			SceneManager.LoadScene("Title");
		}
	}

	public string LevelName() {
		return LevelData.name(levelIndex);
	}

	public void Unlock (int level) {
		if (!unlocked.Contains(level)) {
			unlocked.Add(level);
		}
	}

	public bool isUnlocked(int level) {
		return unlocked.Contains(level);
	}

	public void WillUnlock(int[] unlock) {
		willUnlock = unlock;
	}

	public void FinishLevel(/*PlayerInfo result*/) {
		foreach (int i in willUnlock) {
			Unlock(i);
		}
	}
		
	public void SetResult(int level, LevelResult result) {
		results[level] = result;
	}
	public LevelResult GetResult(int level) {
		return (LevelResult)results[level];
	}
	public void SetCurrentResult(LevelResult result) {
		results[levelIndex] = result;
	}
	public LevelResult GetCurrentResult() {
		return (LevelResult)results[levelIndex];
	}

	public void AdjustTotalCoins(int coinIncrease) {
		coins += coinIncrease;
	}
	public void AdjustTotalDiamonds(int diamondIncrease) {
		diamonds += diamondIncrease;
	}
}
