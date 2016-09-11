using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	// enum MoveType { Step, StepClimb, TurnLeft, TurnRight, TurnAround, Interact };
	enum MoveType { Step, StepClimb, TurnWest, TurnEast, TurnNorth, TurnSouth, Interact };

	public float rotateTime = 0.25f;
	public float walkTime = 1.5f;
	public float liftTime = 0.5f;
	public float fallTime = 0.25f;

	private Queue moveList;
	private PlayerInfo playerInfo;
	private Animator anim;
	private bool moving;

	private Quaternion startRotation;
	private Quaternion targetRotation;
	private Vector3 startPosition;
	private Vector3 targetPosition;
	private Quaternion interactStartRotation;
	private Quaternion interactTargetRotation;
	private Vector3 interactStartPosition;
	private Vector3 interactTargetPosition;

	private float moveTime;
	private float moveTimeLeft;
	private Queue targetRotationList;
	private Queue targetPositionList;
	private Queue moveTimeList;
	private Queue animList;

	private bool interacting; // Currently interacting with an object
	private bool unparent; // After this move, release parent relationship
	private Transform interactTransform;
	private Transform interactOldParent;
	private Animator interactAnim;
	private Queue interactRotationList;
	private Queue interactPositionList;
	private Queue interactAnimList;

	private AreaTracker space;
	private AreaTracker ahead;
	private AreaTracker above;
	private AreaTracker aboveAhead;
	private AreaTracker aboveAbove;
	private AreaTracker aboveAboveAhead;
	private AreaTracker below;

	void Awake () {
		playerInfo = GetComponent<PlayerInfo>();
		anim = GetComponentInChildren<Animator>();
		moveList = new Queue();
		targetRotationList = new Queue();
		targetPositionList = new Queue();
		moveTimeList = new Queue();
		animList = new Queue();
		interacting = false;
		interactRotationList = new Queue();
		interactPositionList = new Queue();
		interactAnimList = new Queue();
		moving = false;
		// TODO: Better way to find the AreaTrackers
		Component[] trackerComponents = GetComponentsInChildren(typeof(AreaTracker));
		if (trackerComponents.Length != 7) {
			print("Error, not six AreaTrackers " + trackerComponents.Length);
		} else {
			space = (AreaTracker)trackerComponents[0];
			ahead = (AreaTracker)trackerComponents[1];
			above = (AreaTracker)trackerComponents[2];
			aboveAhead = (AreaTracker)trackerComponents[3];
			aboveAbove = (AreaTracker)trackerComponents[4];
			aboveAboveAhead = (AreaTracker)trackerComponents[5];
			below = (AreaTracker)trackerComponents[6];
		}
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			moveList.Enqueue(MoveType.TurnWest);
		}
		if (Input.GetKeyDown(KeyCode.RightArrow)) {
			moveList.Enqueue(MoveType.TurnEast);
		}
		if (Input.GetKeyDown(KeyCode.DownArrow)) {
			moveList.Enqueue(MoveType.TurnSouth);
		}
		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			moveList.Enqueue(MoveType.TurnNorth);
		}
		if (Input.GetKeyDown(KeyCode.Space)) {
			moveList.Enqueue(MoveType.Interact);
		}
		if (!moving) {
			move();
		} 
		if (moving) {
			moveTimeLeft -= Time.deltaTime;
			float lerpTime = moveTimeLeft / moveTime;
			transform.localPosition = Vector3.Lerp(targetPosition, startPosition, lerpTime);
			transform.rotation = Quaternion.Lerp(targetRotation, startRotation, lerpTime);
			if (interacting) {
				interactTransform.localPosition = Vector3.Lerp(interactTargetPosition, interactStartPosition, lerpTime);
				interactTransform.rotation = Quaternion.Lerp(interactTargetRotation, interactTargetRotation, lerpTime);
			}
			moving = (moveTimeLeft > 0);
			if (!moving) {
				if (!getNextMoveStep()) {
					//anim.SetTrigger("Stand");
					anim.Play("Idle");
					if (interacting) {
						interacting = false;
						interactAnim.Play("Idle");
					}
					if (unparent) {
						unparent = false;
						unsetParent();
					}
				} else {
					moving = true;
				}
			}
		}
	}

	void move() {

		if (!space.IsEmpty()) {
			if (space.GetObject().CompareTag("Collectible")) {
				playerInfo.Collect(space.GetObject().GetComponent<Collectible>());
			}
		}

		if (below.IsPassable()) {
			addRelativeMove(fallTime, Quaternion.Euler(0, 0, 0), Vector3.down, "Fall");
			return;
		}

		if (moveList.Count == 0) {
			return;
		}

		MoveType move = (MoveType)moveList.Dequeue();
		switch (move) {
			default:
				return;
			/*case MoveType.TurnLeft:
				addRelativeMove(rotateTime, Quaternion.Euler(0, -90, 0), new Vector3(0,0,0), "Turn");
				break;
			case MoveType.TurnRight:
				addRelativeMove(rotateTime, Quaternion.Euler(0, 90, 0), new Vector3(0,0,0), "Turn");
				break;
			case MoveType.TurnAround:
				addRelativeMove(rotateTime, Quaternion.Euler(0, 180, 0), new Vector3(0,0,0), "Turn");
				break;*/
			case MoveType.TurnNorth:
			case MoveType.TurnSouth:
			case MoveType.TurnEast:
			case MoveType.TurnWest:
				if (rotateTo(move)) {
					moveList.Enqueue(MoveType.Step);
				} else {
					moveList.Enqueue(MoveType.StepClimb);
				}
				break;
			case MoveType.Step:
				if (ahead.IsPassable()) {
					if (!carrying()) {
						stepForward();
					} else { // Carrying
						if (aboveAhead.IsEmpty()) {
							stepForward();
						} else { // Drop block behind
							unsetParent();
						}
					}
				}
				break;
			case MoveType.StepClimb:
				if (ahead.IsPassable()) {
					if (!carrying()) {
						stepForward();
					} else { // Carrying
						if (aboveAhead.IsEmpty()) {
							stepForward();
						} else { // Drop block behind
							unsetParent();
							stepForward();
						}
					}
				} else { // Climb
					if (!carrying()) {
						if (aboveAhead.IsPassable() && above.IsPassable()) {
							addRelativeMove(walkTime, Quaternion.Euler(0, 0, 0), new Vector3(0, 1, 0), "Climb");
							addRelativeMove(walkTime, Quaternion.Euler(0, 0, 0), transform.forward, "Clamber");
						}
					} else { // Carrying
						if (aboveAhead.IsPassable() && aboveAbove.IsEmpty() && aboveAboveAhead.IsEmpty()) {
							addRelativeMove(walkTime, Quaternion.Euler(0, 0, 0), new Vector3(0, 1, 0), "Climb");
							addRelativeMove(walkTime, Quaternion.Euler(0, 0, 0), transform.forward, "Clamber");
						}
					}
				}
				break;
			case MoveType.Interact:
				if (carrying()) { // Try to drop the block
					if (aboveAhead.IsEmpty()) {
						if (ahead.IsEmpty()) { // Drop Ahead
							interactWith(above.GetObject());
							addInteractiveMove(liftTime, Quaternion.Euler(0, 0, 0), new Vector3(0, 0, 0), "Drop", Quaternion.Euler(90, 0, 0), new Vector3(0, -1, 1), "GetDropped");
						} else { // Put on top
							interactWith(above.GetObject());
							addInteractiveMove(liftTime, Quaternion.Euler(0, 0, 0), new Vector3(0, 0, 0), "DropHigh", Quaternion.Euler(0, 0, 0), new Vector3(0, 0, 1), "GetDroppedHigh");
						}
						unparent = true;
					}
				} else if (!ahead.IsEmpty() && ahead.GetObject().CompareTag("Block")) { // Pick up the block
					if (above.IsEmpty() && aboveAhead.IsEmpty()) {
						interactWith(ahead.GetObject());
						interactOldParent = interactTransform.parent;
						interactTransform.parent = transform;
						addInteractiveMove(liftTime, Quaternion.Euler(0, 0, 0), new Vector3(0, 0, 0), "PickUp", Quaternion.Euler(-90, 0, 0), new Vector3(0, 1, -1), "GetLifted");
					}
				} else if (!ahead.IsEmpty() && ahead.GetObject().CompareTag("Interactable")) { // Interact
				}
				break;
		}
		if (moving) {
			getNextMoveStep();
		}
	}

	bool rotateTo(MoveType type) {
		Vector3 r;
		switch (type) {
			default:
				r = transform.rotation.eulerAngles;
				break;
			case MoveType.TurnNorth:
				r = new Vector3(0, 0, 0);
				break;
			case MoveType.TurnWest:
				r = new Vector3(0, 270, 0);
				break;
			case MoveType.TurnSouth:
				r = new Vector3(0, 180, 0);
				break;
			case MoveType.TurnEast:
				r = new Vector3(0, 90, 0);
				break;
		}
		Vector3 diff = r - transform.rotation.eulerAngles;
		if (diff.magnitude > 45) {
			addRelativeMove(rotateTime, Quaternion.Euler(diff), new Vector3(0, 0, 0), "Turn");
			return true;
		}
		return false;
	}

	void addRelativeMove(float time, Quaternion rotation, Vector3 position, string anim) {
		moveTimeList.Enqueue(time);
		targetRotationList.Enqueue(rotation);
		targetPositionList.Enqueue(position);
		animList.Enqueue(anim);
		moving = true;
	}

	void addInteractiveMove(float time, Quaternion rotation, Vector3 position, string anim, Quaternion intRotation, Vector3 intPosition, string intAnim) {
		addRelativeMove(time, rotation, position, anim);
		interactRotationList.Enqueue(intRotation);
		interactPositionList.Enqueue(intPosition);
		interactAnimList.Enqueue(intAnim);
	}

	void interactWith(GameObject obj) {
		interacting = true;
		interactTransform = obj.transform;
		interactAnim = obj.GetComponent<Animator>();
		interactPositionList.Clear();
		interactRotationList.Clear();
	}

	void unsetParent() {
		interactTransform.parent = interactOldParent;
		BlockGravity bg = interactTransform.gameObject.GetComponent<BlockGravity>();
		bg.ResetTracker();
	}

	bool getNextMoveStep() {
		// print("getNextMoveStep() " + moveTimeList.Count);
		if (moveTimeList.Count == 0 || targetRotationList.Count == 0 || targetPositionList.Count == 0 || animList.Count == 0) {
			return false;
		}
		if (interacting && (interactRotationList.Count == 0 || interactPositionList.Count == 0 || interactAnimList.Count == 0)) {
			return false;
		}
		setStart();
		moveTime = (float)moveTimeList.Dequeue();
		 print("moveTime dequeued to " + moveTime);
		moveTimeLeft = moveTime;
		targetRotation = startRotation * (Quaternion)targetRotationList.Dequeue();
		targetPosition = startPosition + (Vector3)targetPositionList.Dequeue();
		if (interacting) {
			interactTargetRotation = interactStartRotation * (Quaternion)interactRotationList.Dequeue();
			interactTargetPosition = interactStartPosition + (Vector3)interactPositionList.Dequeue();
			string intClip = (string)interactAnimList.Dequeue();
			// TODO: Check for clip existence
			//if (interactAnim.GetInteger(intClip) >= 0) {
				interactAnim.Play(intClip);
			//} else {
			//	interactAnim.Play("Idle");
			//	print("Unknown interactAnim clip " + intClip);
			//}
		}
		string clip = (string)animList.Dequeue();
		// if (anim.GetInteger(clip) >= 0) {
		anim.Play(clip);
		// } else {
		//	print("Unknown player animator clip " + clip);
		//}
		return true;
	}

	void setStart() {
		startPosition = transform.localPosition;
		startRotation = transform.rotation;
		if (interacting) {
			interactStartPosition = interactTransform.localPosition;
			interactStartRotation = interactTransform.rotation;
		}
	}

	bool carrying() {
		return (!above.IsEmpty() && above.GetObject().CompareTag("Block"));
	}
	void stepForward() {
		addRelativeMove(walkTime, Quaternion.Euler(0,0,0), transform.forward, "Walk");
	}
}
