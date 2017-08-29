using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWall : MonoBehaviour {
	//Configuration
	GameObject config;

    //Rotation speed
    readonly float speed = 50f;

	//Moving walls
	GameObject[] walls;
	GameObject bg; //background
	GameObject sc; //spawnController

    //Rotation direction/destination
	bool rotateDir;
	int currentDir = (int)Config.getCurrentDirection();

    //Direction/Angle properties
    float angleToRotateTo;
    Vector3 startPosition;
    Vector3 destinationPosition;

    void Start()
    {   //Rotation Direction
		rotateDir = (Random.value > 0.5f);
		//Config
		config = GameObject.Find("Configuration");
		//Background
		bg = GameObject.Find("Background");
		//SpawnController
		sc = GameObject.Find("SpawnController");

		//Starting wall position x
		float wallX = GameObject.Find("SpawnPoint_E").transform.position.x;

		//Debug.Log ("Rotation Angle: " + transform.eulerAngles.z);
		if (rotateDir) {
			angleToRotateTo = transform.eulerAngles.z - 90f;
			if (angleToRotateTo < 0) angleToRotateTo = 270;
			currentDir++;
			if (currentDir > 3) currentDir = 0;
		} else {
			angleToRotateTo = transform.eulerAngles.z+ 90f;
			if (angleToRotateTo >= 360) angleToRotateTo = 0f;
			currentDir--;
			if (currentDir < 0) currentDir = 3;
		}

		switch ((Config.Direction)currentDir) {
				case Config.Direction.north:
					destinationPosition = new Vector3 (0, -wallX);
					break;
				case Config.Direction.south:
					destinationPosition = new Vector3 (0, wallX);
					break;
				case Config.Direction.east:
					destinationPosition = new Vector3 (-wallX, 0);
					break;
				case Config.Direction.west:
					destinationPosition = new Vector3 (wallX, 0);
					break;
			}

		Config.setCurrentDirection ((Config.Direction)currentDir);
    }

    // Update is called once per frame
    void Update() {
		//Debug.Log ("Current Angle :" + (int)transform.eulerAngles.z + "   goal Angle: " + angleToRotateTo);
        if ((int)this.transform.position.x == 0 && (int)this.transform.position.y == 0)
		{
			sc.GetComponent<SpawnController> ().stopSpawn ();
			rotateWall(rotateDir, (Config.Direction)currentDir);
        }
    }

	/*
	 * 
	 * Rotate All the walls
	 */ 

    void rotateWall(bool cw, Config.Direction d)
    {
            //Rotation direction
            Vector3 rotDir = (cw) ? Vector3.back : Vector3.forward;

            //Stop Walls moving & Spawning
			config.GetComponent <Config>().setSpeedZero();
            //bg.GetComponent<SpawnDestroyWall>().stopSpawns();

            //control walls
            walls = GameObject.FindGameObjectsWithTag("Wall");

			Vector3 to = new Vector3(0,0,angleToRotateTo);
			if(Vector3.Distance(transform.eulerAngles,to) > 2f)
            {
			Debug.Log (Vector3.Distance (transform.eulerAngles, to));
			     //Start Rotating
                foreach (GameObject w in walls)
                {
					w.transform.RotateAround(Vector3.zero, rotDir, speed * Time.deltaTime);
                }

            } else {
				//Last Rotation
				foreach (GameObject w in walls)
				{
					w.transform.Rotate (new Vector3 (0,0,angleToRotateTo));
				}

				destroyRotation (d);
            }
    }

 
	/*
	 * 
	 * Setup new settings, wall position and rotation
	 */
	void destroyRotation(Config.Direction d) {
		//Set everything upto start
		//Reallign all walls depending on direction
		Debug.Log(d);
		foreach (GameObject w in walls)
		{
			if (d == Config.Direction.north || d == Config.Direction.south) {
				w.transform.position = new Vector3 (0, w.transform.position.y);
			} else {
				w.transform.position = new Vector3(w.transform.position.x, 0);
			}
			w.transform.eulerAngles = new Vector3(0, 0, angleToRotateTo);
		}

		//Change spawn locations
		//bg.GetComponent<SpawnDestroyWall>().setSpawnLocation(startPosition);
		sc.GetComponent<SpawnController>().startSpawn(d);

		//Change movement - Destination
		bg.GetComponent<MovingWall>().setTarget(destinationPosition);
		Debug.Log (destinationPosition);

		//Change obj rotation of spawned object
		sc.GetComponent<SpawnController>().setPieceRotation(Quaternion.Euler(0, 0, angleToRotateTo));

		//Start Movement
		config.GetComponent<Config>().setupSpawnSpeed();
		Destroy(this);
	}

}
