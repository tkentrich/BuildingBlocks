using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelSelector : MonoBehaviour {

	public GameObject levelPrefab;

	GameProgress prog;
	ArrayList levelObjects;

	int[] levels;
	int selectedIndex;

	void Start () {
		prog = Object.FindObjectOfType<GameProgress>();
		levelObjects = new ArrayList();
		levels = LevelData.CampaignLevels();
		int index = 0;
		foreach (int i in levels) {
			GameObject level = Instantiate(levelPrefab);
			level.GetComponent<LevelMover>().index = index++;
			level.transform.SetParent(transform);
			level.transform.localPosition.Set(0, 0, 0);
			level.GetComponent<Text>().text = LevelData.name(i);
			levelObjects.Add(level);
		}
		Select(0);
	}

	public void Select (int index) {
		if (index < 0) {
			index = 0;
		}
		if (index >= levels.Length) {
			index = levels.Length - 1;
		}
		selectedIndex = index;
		foreach (GameObject obj in levelObjects) {
			obj.GetComponent<LevelMover>().SetPosition(selectedIndex);
		}
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			Select(selectedIndex - 1);
		}
		if (Input.GetKeyDown(KeyCode.DownArrow)) {
			Select(selectedIndex + 1);
		}
		if (Input.GetKeyDown(KeyCode.Return) && prog.isUnlocked(levels[selectedIndex])) {
			prog.levelIndex = levels[selectedIndex];
			SceneManager.LoadScene("Level");
		}
	}
}
