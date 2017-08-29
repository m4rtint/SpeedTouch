using UnityEngine;
using System.Collections;

public class BreakBridge : MonoBehaviour {

	GameObject bridge;
	void Start() {
		//bridge = GameObject.Find ("bridge");
	}

	void OnTriggerEnter2D(Collider2D obj) {
		if (obj.gameObject.tag == "Player") {
			foreach(Transform child in gameObject.transform.parent) {
				Destroy (child.gameObject);
			}
		}
	}
}
