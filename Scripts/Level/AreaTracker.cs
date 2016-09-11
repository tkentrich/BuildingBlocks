using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AreaTracker : MonoBehaviour {

	public string identifier;

	ArrayList objects;
	List<string> ignore;

	void Awake () {
		objects = new ArrayList ();
		if (ignore == null) {
			ignore = new List<string>();
		}
	}

	void OnTriggerEnter(Collider other) {
		if (!objects.Contains(other.gameObject) && (!ignore.Contains(other.tag)) && 
			(other.CompareTag("Brick") || other.CompareTag("Block") || other.CompareTag("Exit") || other.CompareTag("Wall") || other.CompareTag("Player") || other.CompareTag("Collectible"))) {
			//print(name + " now contains " + other.tag);
			objects.Add(other.gameObject);
		}
	}

	void OnTriggerExit(Collider other) {
		if (objects.Contains(other.gameObject)) 
			objects.Remove(other.gameObject);
	}

	public void Ignore(string tagIgnore) {
		if (ignore == null) {
			ignore = new List<string>();
		}
		ignore.Add(tagIgnore);
		foreach (GameObject obj in objects) {
			if (obj.CompareTag(tagIgnore)) {
				objects.Remove(obj);
			}
		}
	}

	public bool IsEmpty() {
		return (objects.Count == 0);
	}

	public bool IsPassable() {
		foreach (GameObject obj in objects) {
			if (obj.CompareTag("Brick") || obj.CompareTag("Block") || obj.CompareTag("Wall") || obj.CompareTag("Player")) {
				return false;
			}
		}
		return true;
	}

	public GameObject GetObject() {
		if (IsEmpty()) {
			return null;
		}
		return ((GameObject)objects.ToArray()[0]);
	}
}
