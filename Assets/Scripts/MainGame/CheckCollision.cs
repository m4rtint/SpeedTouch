using UnityEngine;
using System.Collections;

public class CheckCollision : MonoBehaviour {

	GameObject gs;

    void Start() {
        gs = GameObject.Find ("Background");
	}

	void OnTriggerEnter2D(Collider2D obj) {
		if (obj.gameObject.tag == "Player") {
			GameObject ds = (GameObject)Resources.Load ("DeathSpot");
			Instantiate (ds,obj.transform.position,Quaternion.Euler(0, 0, 0));
			gs.GetComponent<GameState> ().setGameLost ();
		}
	}

	void Update() {
		//Destroy wall when reach certain point
		if (this.transform.position == gs.GetComponent<MovingWall>().getTarget()) {
			Destroy(gameObject);
		}
	}
}
