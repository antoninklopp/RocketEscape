using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

/// <summary>
/// GameManager from the levels scene.
/// </summary>
public class GameManagerLevels : MonoBehaviour {

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            SceneManager.LoadScene("Menu");
        }
    }

    public void GoToMenu() {
        SceneManager.LoadScene("Menu");
    }

    public void GoToLevel(int number){
        PlayerPrefs.SetInt("Level", number);
        SceneManager.LoadScene(number.ToString() + "D"); 
    }
}
