using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {
    //background gameobject
	GameObject config;

    // Use this for initialization
    void Start () {
		//Config scripts
		config = GameObject.Find("Configuration");
        //Start Movement
		config.GetComponent<Config>().setupSpawnSpeed();
        //Start Spawn
        //this.GetComponent<SpawnDestroyWall>().startSpawns();
    }
}
