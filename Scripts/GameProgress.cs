using UnityEngine;
using UnityEngine.SceneManagement;

using System.Collections;

public class GameProgress : MonoBehaviour {

	ArrayList unlocked;
	Hashtable results;

	void Start () {
		DontDestroyOnLoad(gameObject);
		unlocked = new ArrayList();
		results = new Hashtable();
		LevelData.InitData();
		SceneManager.LoadScene("Title");
	}

	void Unlock (string level) {
		if (!unlocked.Contains(level)) {
			unlocked.Add(level);
		}
	}
	
}
