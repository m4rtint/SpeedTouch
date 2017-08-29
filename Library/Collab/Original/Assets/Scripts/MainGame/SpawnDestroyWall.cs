using UnityEngine;
using System.Collections;
using System.IO;

public class SpawnDestroyWall : MonoBehaviour {
    //object to spawn
    GameObject wall;

    //Spawn Position
    private float startingWallPositionx = 25.6f;
    public float getStartingWallPositionx() //Get
    {
        return startingWallPositionx;
    }
    private Vector3 spawnLocation;
    public void setSpawnLocation(Vector3 v) //Set
    {
        spawnLocation = v;
    }

    //Spawn Rotation
    Quaternion pieceRotation;
    public void setPieceRotation(Quaternion pr) //Set
    {
        pieceRotation = pr;
    }

	//Spawn Rate
	float spawnRate;

	//Wall Units (Amount of Wall Sprite available)
	readonly int wallAmount = 6;
    int unit;

    void Start() {
		//SpawnRate
		spawnRate = GameObject.Find("Configuration").GetComponent<Config>().getSpawnRate();

        //Set Spawn Location
		spawnLocation = new Vector3 (startingWallPositionx * 2, 0);

        //Spawn Rotation
        pieceRotation = Quaternion.Euler(0, 0, 0);
	}

	void spawnWall() {
		Debug.Log (spawnRate);
		unit = Random.Range (1, wallAmount+1);
        //Setup rotation direction - clockwise/anticlockwise
        wall = (GameObject)Resources.Load("Walls/m_wall_" + unit + "_m");
        if (Random.value > 0.8f && GameObject.Find("Background").GetComponent<GameState>().getTime() > 5)
            wall = (GameObject)Resources.Load("Walls/m_wall_r_m");
       
        Instantiate (wall, spawnLocation,pieceRotation);
	}

	public void stopSpawns() {
		CancelInvoke ();
	}

	public void startSpawns(float starting = 0) {
		InvokeRepeating ("spawnWall", starting, spawnRate);
	}

}
