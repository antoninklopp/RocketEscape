using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

/// <summary>
/// Game Manager from the credits scene. 
/// </summary>
public class GameManagerCredits : MonoBehaviour {

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            SceneManager.LoadScene("Menu"); 
        }
    }

    public void GoToMenu() {
        SceneManager.LoadScene("Menu");
    }
}
