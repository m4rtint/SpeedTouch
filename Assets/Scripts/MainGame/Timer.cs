using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
    //GameObjects
    GameObject gs;
    void Start()
    {
        gs = GameObject.Find("Background");
    }
	
	// Update is called once per frame
	void Update () {
        float timer = gs.GetComponent<GameState>().getTime();
        
        GameObject.Find("Timer").GetComponent<Text>().text = "Time " + timer.ToString("F2");
    }
}
