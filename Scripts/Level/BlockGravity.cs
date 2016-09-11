using UnityEngine;
using System.Collections;

public class BlockGravity : MonoBehaviour {

	public static float fallTime = 0.25f;

	AreaTracker tracker;
	bool falling = false;
	float fallTimeLeft;
	private Vector3 startPosition;
	private Vector3 targetPosition;

	public void ResetTracker() {
		tracker.transform.position = transform.position + Vector3.down;
	}

	void Start () {
		tracker = gameObject.GetComponentInChildren<AreaTracker>();
	}

	void Update () {
		ResetTracker();
		if (!falling && tracker.IsEmpty() && !transform.parent.gameObject.CompareTag("Player")) {
			falling = true;
			startPosition = transform.localPosition;
			targetPosition = startPosition + new Vector3(0, -1, 0);
			fallTimeLeft = fallTime;
		}
		if (falling) {
			fallTimeLeft -= Time.deltaTime;
			if (fallTimeLeft > 0f) {
				transform.localPosition = Vector3.Lerp(targetPosition, startPosition, fallTimeLeft / fallTime);
			} else {
				transform.localPosition = targetPosition;
				falling = false;
			}
		}
	}
}
