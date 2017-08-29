using UnityEngine;
using System.Collections;

public class MovingWall : MonoBehaviour {

    //Moving walls
    GameObject[] walls;

    //Positions
    float destinationWallPositionx = -52f; //Destinations

    Vector3 target;
    public void setTarget(Vector3 t)
    {
        target = t;
    }
    public Vector3 getTarget()
    {
        return target;
    }

	// Use this for initialization
	void Start () {
        target = new Vector3(destinationWallPositionx, 0);
    }
	
	// Update is called once per frame
	void Update () {
		walls = GameObject.FindGameObjectsWithTag ("Wall");
		foreach (GameObject w in walls) {
            float speed = GameObject.Find("Configuration").GetComponent<Config>().getSpeed();
            w.transform.position = Vector3.MoveTowards (w.transform.position, target, speed * Time.deltaTime);
		}
	}


}
 