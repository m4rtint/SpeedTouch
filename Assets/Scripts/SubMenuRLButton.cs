using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubMenuRLButton : MonoBehaviour {
    //Configurations
    GameObject config;

    //Moving walls
    GameObject[] walls;
    Hashtable wallTable = new Hashtable();

    //Right - True
    //Left - False
    public bool RightLeftDirection = true;

    //Wall
    GameObject wall;

    //Direction
    int direction;

    //Animation Movement
    bool movement = false;
    bool animationTimer = true;

	//Audio Component
	AudioSource sfx;

    // Use this for initialization
    void Start()
    {
        config = GameObject.Find("Configuration");
		//Setup Sound
		sfx = GetComponent<AudioSource>();
        //Setup Directions
        direction = (RightLeftDirection) ? -1 : 1;
        //Setup Mode
        ChangeMode((int)Config.Mode.normal);

    }

    void OnMouseDown()
    {
        //Do not allow click while mid animation
        if (animationTimer)
        {
            animationTimer = false;
            //Animation menu movement
            Vector3 currentPos = this.transform.position;
            this.transform.position = new Vector3(currentPos.x - direction * 0.05f, currentPos.y - 0.05f, -5);

            //Mode configurations   
            int currentMode = (int)config.GetComponent<Config>().getCurrentMode();

            currentMode -= direction;
            if (currentMode > 3)
                currentMode = 0;
            else if (currentMode < 0)
                currentMode = 3;

            config.GetComponent<Config>().setCurrentMode(currentMode);

            //Animation of Menu
            ChangeMenu();
            ChangeMode(currentMode);

            //Configuration of speeds and spawnrate
            GameObject.Find("Configuration").GetComponent<Config>().setMode(currentMode);

			//Play sound
			sfx.Play();
        }
    }

    private void OnMouseUp()
    {

        Vector3 currentPos = this.transform.position;
        this.transform.position = new Vector3(currentPos.x + direction*0.05f, currentPos.y + 0.05f,-5);
    }

    void ChangeMenu()
    {
        //Move left or right on the Menu
        Vector3 spawnLocation = new Vector3(direction * 25.6f, 0, 0);
        
        //Create wall
        int unit = Random.Range(1, 7);
        wall = (GameObject)Resources.Load("Walls/m_wall_" + unit + "_m");
        GameObject newWall = Instantiate(wall, spawnLocation, transform.rotation);

        //Remove scripts
        Destroy(newWall.GetComponent<CheckCollision>());

        //Collect all wals
        walls = GameObject.FindGameObjectsWithTag("Wall");

        //Clear Hashtable
        wallTable.Clear();
        foreach (GameObject w in walls)
        {
            wallTable.Add(w, new Vector3(w.transform.position.x - 25.6f * direction, 0 ,0));
        }

        movement = true;
    }
    
    void ChangeMode(int cM)
    {
        string logo = "speedy_sprite";
        string mode = "normal";
        switch (cM)
        {
            case 0:
                logo = "speedy_sprite";
                mode = "normal"; break;
            case 1:
                logo = "speedier_sprite";
                mode = "hard"; break;
            case 2:
                logo = "speediest_sprite";
                mode = "superhard"; break;
            case 3:
                logo = "extreme_sprite";
                mode = "exetreme"; break;
            default:
                logo = "speedy_sprite";
                mode = "normal";
                break;
        }
        GameObject.Find("logo").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Logo/" + logo);
        float timer = PlayerPrefs.GetFloat(mode, 0f);
        GameObject.Find("Text").GetComponent<Text>().text = mode+"\n"+ timer.ToString("F2");

    }

    void Update()
    {
       if (movement)
        {
            foreach (GameObject w in walls) {
                w.transform.position = Vector3.MoveTowards(w.transform.position, (Vector3)wallTable[w], 100 * Time.deltaTime);
                if (w.transform.position.x == (-25.6f * direction))
                {
                    Destroy(w);
                    movement = false;
                    animationTimer = true;
                }
            }
            
        }

    }

}
