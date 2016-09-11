using UnityEngine;
using System.Collections;

public class LevelSelector : MonoBehaviour {

	Hashtable levels;

	void Start () {
		levels = LevelData.LevelNames();
	}

	void Update () {
	
	}

	public void Select (int index) {
	}
}
