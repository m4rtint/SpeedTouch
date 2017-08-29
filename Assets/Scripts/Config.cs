using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config : MonoBehaviour {

    readonly float normalSpeed = 13f;
 
    readonly float hardSpeed = 18f;

    readonly float superHardSpeed = 18f;
   
    readonly float extremeSpeed = 23f;
   
	//Wall Pieces
	readonly int wallAmount = 9;
	public int getWallAmount() {
		return wallAmount;
	}


    //Speed Properties
    float speed;
    public float getSpeed() //get
    {
        return speed;
    }
    public void setSpeedZero() ///set
    {
        speed = 0f;
    }

    public enum Mode
    {
        normal,
        hard,
        superhard,
        extreme,
    };

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

	//Current Direction
	public enum Direction{
		north,
		east,
		south,
		west 
	};
	static Direction currentDirection = Direction.east;
	public static Direction getCurrentDirection(){
		return currentDirection;
	}
	public static void setCurrentDirection(Direction d){
		currentDirection = d;
	}

	//Curent Mode of game
    Mode currentMode = Mode.normal;
    public Mode getCurrentMode()
    {
        return currentMode;
    }
    public void setCurrentMode(int i)
    {
        currentMode = (Mode)i;
    }
    
    public void setMode(int c)
    {
        switch (c)
        {
            case 0:
                currentMode = Mode.normal;
                break;
            case 1:
                currentMode = Mode.hard;
                break;
            case 2:
                currentMode = Mode.superhard;
                break;
            case 3:
                currentMode = Mode.extreme;
                break;
            default:
                currentMode = Mode.normal;
                break;
        }
		setupSpawnSpeed ();

    }

    public void setupSpawnSpeed()
	{ 
        switch(currentMode)
        {
            case Mode.normal:
                speed = normalSpeed;
                break;
            case Mode.hard:
                speed = hardSpeed;
                break;
            case Mode.superhard:
                speed = superHardSpeed;
                break;
            case Mode.extreme:
                speed = extremeSpeed;
                break;
            default:
                speed = normalSpeed;
                break;
        }

    }
	
}
