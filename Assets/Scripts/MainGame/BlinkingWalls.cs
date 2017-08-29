using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingWalls : MonoBehaviour {

	//Moving walls
	GameObject[] walls;

	public string fromColor;
	public string toColor;

	Color myColor;
	Color lerpedColor;
	Color switchedColors;

	private void Start()
	{
		if (fromColor == null) 
			fromColor = "#FFFF00FF";
		if (toColor == null)
			toColor = "#FF0000FF";
		
		myColor = new Color();
		lerpedColor = new Color ();
		ColorUtility.TryParseHtmlString(fromColor, out myColor);

		ColorUtility.TryParseHtmlString (toColor, out lerpedColor);
	}


	// Update is called once per frame
	void Update () {
		walls = GameObject.FindGameObjectsWithTag ("Wall");
		foreach (GameObject w in walls) {
			switchedColors = Color.Lerp(lerpedColor,myColor, Mathf.PingPong(Time.time, 1));
			w.GetComponent<SpriteRenderer>().color = switchedColors;
		}

	}

}
