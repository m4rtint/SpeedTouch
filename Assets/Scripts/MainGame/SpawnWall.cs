using UnityEngine;
using System.Collections;
using System.IO;

public class SpawnWall : MonoBehaviour {
    //object to spawn
	GameObject sc;

	void Start() {
		sc = GameObject.Find ("SpawnController");
	}

	void OnTriggerExit2D(Collider2D other) {
		if (this.enabled) {
			//Get Wall Name to Generate
			string wallName = sc.GetComponent<SpawnController> ().generateWall ();
			//Load up wall form Resource
			GameObject wall = (GameObject)Resources.Load (wallName);
			//Setup Rotation angle
			Quaternion rot = sc.GetComponent<SpawnController> ().getPieceRotation ();

			Instantiate (wall, this.transform.position, rot);
		}
	
	}
}
