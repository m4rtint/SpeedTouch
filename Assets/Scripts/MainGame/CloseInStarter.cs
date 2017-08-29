using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseInStarter : MonoBehaviour {
    bool objectAnimationMovement = false;

    //Scale of the object at the beginning of the animation
    Vector3 actualScale;
    //Scale of the object at the end of the animation
    Vector3 targetScale;

    readonly float time = 2f;
    float currentTime = 0.0f;

    private void Start()
    {
        actualScale = transform.localScale;
        targetScale = new Vector3(1.0f, 1.0f, 1.0f);
    }

    public void setObjectAnimationMovement()
    {
        objectAnimationMovement = true;
    }

    
	// Update is called once per frame
	void Update () {
        if (objectAnimationMovement)
        { 
            transform.localScale = Vector3.Lerp(actualScale, targetScale, currentTime / time);
            currentTime += Time.deltaTime;

            if(this.transform.localScale.y == 1f)
            {
                Destroy(this);
            }
        }
	}

    
}
