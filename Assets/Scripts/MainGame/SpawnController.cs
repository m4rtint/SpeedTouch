using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {
	int wallAmount = 0;

	//Last wall that spawned
	string lastWall = "m_wall_starter_m";

	//Wall Rotation activate
	bool activateRotation = true;
	int countDownRotationReactivation = 0;
	bool modeAllowRotation = false;

	//Current Spawn Location
	GameObject spawnPoint;

	//Spawn Rotation
	Quaternion pieceRotation;
	public void setPieceRotation(Quaternion pr) { pieceRotation = pr;}
	public Quaternion getPieceRotation(){return pieceRotation;}


	//Setup wall to Spawn
	void Start () {
		//Get Current SpawnPoint GameObject
		spawnPoint = GameObject.Find("SpawnPoint_E");

		//Spawn Rotation
		pieceRotation = Quaternion.Euler(0, 0, 0);

		//Configuration
		wallAmount = GameObject.Find("Configuration").GetComponent<Config>().getWallAmount();

		//Current Mode
		Config.Mode mode = GameObject.Find("Configuration").GetComponent<Config>().getCurrentMode();
		if (mode == Config.Mode.extreme || mode == Config.Mode.superhard)
			modeAllowRotation = true;
	}

	/*
     * Generate the name for the next wall to spawn
     * Sample wall name - m_wall_4_m
     */
	public string generateWall() {
		string[] properties = lastWall.Split ('_');
		string end = "m";

		/*
            float RNG = Random.value;
            if (RNG < 0.3f) {
                end = "b";
            } else if (RNG >= 0.3f && RNG <= 0.6) {
                end = "m";	
            } else if (RNG > 0.6f) {
                end = "t";
            }
            */

		//Setup WallName
		int unit = Random.Range (1, wallAmount);
		lastWall =  properties [3] + "_wall_" + unit +"_"+ end;

		//Rotation Settings
		if (countDownRotationReactivation <= 0)
			activateRotation = true;
		else
			countDownRotationReactivation--;

		if (activateRotation && Random.value > 0.8f && modeAllowRotation) {
			lastWall = "m_wall_r_m";
			countDownRotationReactivation = 5;
			activateRotation = false;
		}

		return "Walls/" +lastWall;
	}

	public void stopSpawn() {
		spawnPoint.GetComponent<SpawnWall>().enabled = false;
	}
		
	public void startSpawn(Config.Direction dir) {
		if (spawnPoint.GetComponent<SpawnWall> ().enabled) {
			spawnPoint.GetComponent<SpawnWall> ().enabled = false;
		}
		string spName = "SpawnPoint_E";
		switch (dir) {
		case Config.Direction.east:
			spName = "SpawnPoint_E";
			break;
		case Config.Direction.west:
			spName = "SpawnPoint_W";
			break;
		case Config.Direction.north:
			spName = "SpawnPoint_N";
			break;
		case Config.Direction.south:
			spName = "SpawnPoint_S";
			break;
		default:
			spName = "SpawnPoint_W";
			break;
		}
		spawnPoint = GameObject.Find (spName);
		spawnPoint.GetComponent<SpawnWall> ().enabled = true;
	}

}
