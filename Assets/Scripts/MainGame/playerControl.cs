using UnityEngine;
using System.Collections;

public class playerControl : MonoBehaviour {

	//Positions
	Vector3 circlePos;
	Vector3 mousePoint;

	//Cameras
	Camera cam;


	// Use this for initialization
	void Start () {
		cam = GameObject.Find("MainCamera").GetComponent<Camera>();
	}

	void Update() {
		//God mode
		if (Input.GetKeyDown ("space"))
			GetComponent<Collider2D> ().enabled = false;

	}
	void OnMouseDown() {
		this.GetComponent<CircleCollider2D> ().radius = 1.0f;
		GameObject.Find ("Background").GetComponent<GameState> ().setGameStart ();
	}

	void OnMouseUp(){
		GameObject.Find ("Background").GetComponent<GameState>().setGameLost();
		Destroy (gameObject);
	}
		
	void OnMouseDrag() {
		mousePoint = cam.ScreenToWorldPoint(Input.mousePosition);
		circlePos = new Vector3 (mousePoint.x, mousePoint.y);
		this.transform.position =  circlePos;
	}



}
