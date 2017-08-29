using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckConfig : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (!GameObject.Find ("Configuration")) {
			GameObject config =(GameObject) Resources.Load ("Configuration");
			GameObject c = Instantiate (config);
			c.name = "Configuration";
		}
		Destroy (gameObject);
	}
}
