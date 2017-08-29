using UnityEngine;
using System.Collections;

public class CheckCollision : MonoBehaviour {

	GameObject gs;

	void Start() {
		gs = GameObject.Find ("Background");

	}

	void OnTriggerEnter2D(Collider2D obj) {
		if (obj.gameObject.tag == "Player") {
			gs.GetComponent<GameState> ().setGameLost ();
			Debug.Log ("Entered wall");
		}
	}

	void Update() {
		//Destroy wall when reach certain point
		if (this.transform.position.x == MovingWall.destinationWallPositionX) {
			Destroy(gameObject);
		}
	}
}
