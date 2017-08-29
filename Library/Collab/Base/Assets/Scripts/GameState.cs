using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour {
    //background scripts
    GameObject bg;

    //Timer
    private  float time = 0;
    public  float getTime()
    {
        return time;
    }

    private bool startTime = false;

    private void Start()
    {
        bg = GameObject.Find("Background");
    }

    void Update()
    {
        if (startTime)
            time += Time.deltaTime;

        Debug.Log(time);
    }

    public void setGameStart() {
        //Start Movement
        bg.GetComponent<MovingWall>().setSpeed(10f);
        //Start Spawn
        bg.GetComponent<SpawnDestroyWall>().startSpawns(0f);
        //Start Timer
        startTime = true;

    }


	public void setGameLost() {
        //Start Movement
        bg.GetComponent<MovingWall>().setSpeed(0f);
        GameObject.Find ("Background").GetComponent<SpawnDestroyWall> ().stopSpawns ();
		Destroy (GameObject.Find ("circle"));
        startTime = false;
		Instantiate ((GameObject)Resources.Load ("retry_button"), Vector3.zero, transform.rotation);
	}
}
