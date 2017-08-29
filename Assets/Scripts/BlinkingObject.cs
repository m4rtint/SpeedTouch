using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingObject : MonoBehaviour {

    public Color myColor;
    private void Start()
    {
        myColor = new Color();
        ColorUtility.TryParseHtmlString("#FFFFFF32", out myColor);
    }

    public Color lerpedColor = Color.white;
    // Update is called once per frame
    void Update () {
       
        lerpedColor = Color.Lerp(Color.white,myColor, Mathf.PingPong(Time.time, 1));
        this.GetComponent<SpriteRenderer>().color = lerpedColor;
    }
}
