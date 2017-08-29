using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour {

    void OnMouseDown()
    {
        if (SceneManager.GetActiveScene().name == "Menu" || SceneManager.GetActiveScene().name == "MainGame")
            SceneManager.LoadScene("SubMenu");
        else
            SceneManager.LoadScene("MainGame");
    }
}
