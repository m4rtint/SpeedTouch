using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameState : MonoBehaviour {
    //Circle gameObject
    GameObject circle;

    //Config gameObject
    GameObject config;

    //StarterWall
    GameObject starterWall;

	//Background Audio music
	AudioSource music;

    //Timer
	private float time = 0;
    public  float getTime()
    {
        return time;
    }
	//Time gotten from last game
	private static float lastTime = 0;

    private bool startTime = false;

    private void Start()
  {
		music = GetComponent<AudioSource> ();
        config = GameObject.Find("Configuration");
        circle = GameObject.Find("circle");
        starterWall = GameObject.Find("starter_wall_m");

        //Each game starts with no movement
        config.GetComponent<Config> ().setSpeedZero ();

		//Start music
		//Start music
		SetMusic();
    }

    void Update()
    {
        if (startTime)
            time += Time.deltaTime;
    }

    public void setGameStart() {
        //Remove Animation of player
        Destroy(circle.GetComponent<Animator>());
        //Destroy Sprite
        circle.GetComponent<SpriteRenderer>().sprite = null;
        //Start Movement
        config.GetComponent<Config>().setupSpawnSpeed();
        //Start Timer
        startTime = true;
        //Wall Closing in Scale
        starterWall.GetComponent<CloseInStarter>().setObjectAnimationMovement();

    }


	public void setGameLost() {
		//Stop Music
		music.Stop();
        //Get Set highscore - Set up time logo
        float currentHighScore = PlayerPrefs.GetFloat(config.GetComponent<Config>().getCurrentMode().ToString(),0f);
        string currentTime = "BEST " + currentHighScore.ToString("F2") + "\nTIME " + time.ToString("F2");
        if (time > currentHighScore)
        {
            currentTime = "NEW BEST " + time.ToString("F2");
            PlayerPrefs.SetFloat(config.GetComponent<Config>().getCurrentMode().ToString(), time);
        }
		lastTime = time;


        //Stop Movement
        config.GetComponent<Config>().setSpeedZero();
        Destroy (GameObject.Find ("circle"));
        startTime = false;

		//User Interface Setup
		setupUI (currentTime);

		//Audio GameLost
		GetComponent<AudioSource>().clip = Resources.Load("Audio/gameloss") as AudioClip;
		GetComponent<AudioSource> ().loop = false;
		music.Play ();

    }

	void SetMusic() {
		string songName = "Audio/";
		float startTime = 0f;
		switch (config.GetComponent<Config> ().getCurrentMode ()) {
		case Config.Mode.normal:
			startTime =  41.118f;
			songName += "StayCrunchy";
			break;
		
		case Config.Mode.hard:
			startTime = 97.569f;
			songName += "GuitarSound";
			break;

		case Config.Mode.superhard:
			startTime = 68.126f;
			songName += "Renegade";
			break;

		case Config.Mode.extreme:
			startTime = 68.126f;
			songName += "Renegade";
			break;

		default:
			startTime = 41.118f;
			songName += "StayCrunchy";
			break;
			
		}
		music.time = (lastTime > 30f) ? startTime : 0f;
		GetComponent<AudioSource>().clip = Resources.Load(songName) as AudioClip;
		GetComponent<AudioSource> ().loop = true;
		music.Play ();
	}

	/*
	 * 
	 * Setup User Interface 
	 * With Time obtained
	 */ 
	private void setupUI(string currentTime) {
		//Respawn Button
		Instantiate ((GameObject)Resources.Load ("Buttons/RetryBtn"));

		//Change Mode Button
		Instantiate((GameObject)Resources.Load("Buttons/changeModeBtn"));

		//Best Time Logos - Text
		GameObject bestTimeGO = Instantiate((GameObject)Resources.Load("Logo/BestTimeUI"));
		bestTimeGO.GetComponent<Text>().text = currentTime;
		bestTimeGO.transform.SetParent(GameObject.Find("Canvas").transform,false);

		//UI
		Instantiate((GameObject)Resources.Load("Logo/bckgrndUI"));
	}
}
