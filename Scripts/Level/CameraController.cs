using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public enum CameraType { Behind, Overhead, Freelance, Round };

	public Transform focus;

	CameraType type;
	Vector3 targetPosition;
	Quaternion targetRotation;

	public float behindDistance;
	public float overheadDistance;
	public Vector3 freelanceVector;
	public float roundMagnitude;
	public Quaternion roundDirection;

	void Awake() {
		type = CameraType.Behind;
	}

	public void SetFocus(Transform focus) {
		this.focus = focus;
	}
		
	void Update () {
		if (focus == null) {
			return;
		}

		if (Input.GetKeyDown(KeyCode.Return)) {
			switch (type) {
				case CameraType.Behind:
					type = CameraType.Overhead;
					break;
				case CameraType.Overhead:
					type = CameraType.Freelance;
					break;
				case CameraType.Freelance:
					type = CameraType.Round;
					break;
				case CameraType.Round:
					type = CameraType.Behind;
					break;
			}
		}
		if (Input.GetKeyDown(KeyCode.PageUp)) {
			switch (type) {
				case CameraType.Behind:
					behindDistance -= 1;
					if (behindDistance < 0) {
						behindDistance = 0;
					}
					break;
				case CameraType.Overhead:
					overheadDistance -= 1;
					if (overheadDistance < 0) {
						overheadDistance = 0;
					}
					break;
				case CameraType.Freelance:
					if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
						freelanceVector.z += 1;
					} else if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) {
						freelanceVector.y += 1;
					} else {
						freelanceVector.x += 1;
					}
					break;
				case CameraType.Round:
					break;
			}
		}
		if (Input.GetKeyDown(KeyCode.PageDown)) {
			switch (type) {
				case CameraType.Behind:
					behindDistance += 1;
				if (behindDistance < 0) {
							behindDistance = 0;
					}
					break;
				case CameraType.Overhead:
					overheadDistance += 1;
					if (overheadDistance < 0) {
						overheadDistance = 0;
					}
					break;
				case CameraType.Freelance:
					if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
						freelanceVector.z -= 1;
					} else if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) {
						freelanceVector.y -= 1;
					} else {
						freelanceVector.x -= 1;
					}
					break;
			}
		}
		switch (type) {
			case CameraType.Behind:
				targetPosition = focus.position - Vector3.forward * behindDistance + focus.up * .5f;
				targetRotation = Quaternion.Euler(0, 0, 0);
				break;
			case CameraType.Overhead:
				targetPosition = focus.position + focus.up * overheadDistance;
				targetRotation = Quaternion.Euler(90, 0, 0);
				break;
			case CameraType.Freelance:
				targetPosition = focus.position + freelanceVector;
				break;
		}

		transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, 0.2f);
		if (type == CameraType.Freelance) {
			transform.LookAt(focus);
		}  else {
			transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.2f);
		}
	}
}
