using UnityEngine;
using System.Collections;

public class Collectible : MonoBehaviour {

	public PlayerInfo.CollectibleType type;
	public int quantity = 1;

	Animator anim;

	public void Start() {
		anim = GetComponent<Animator>();
		anim.Play("Idle");
	}

	public void Collect(out PlayerInfo.CollectibleType type, out int quantity) {
		type = this.type;
		quantity = this.quantity;
		anim.Play("Collect");
		this.tag = "Untagged";
	}

	public void RemoveCollectible() {
		anim.Stop();
		gameObject.SetActive(false);
	}
}
