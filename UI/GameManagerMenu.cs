using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Game Manager from the menu. 
/// </summary>
public class GameManagerMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (!PlayerPrefs.HasKey("Level")) {
            PlayerPrefs.SetInt("Level", 1); 
        }
	}
	
	// Update is called once per frame
	void Update () {
    /* 
     * if we are not in a Web gl built, we can quit thee game by pressing the escaoe key. 
     * if the player plays on a browser, he can easily quit the game by closing the browser.
     * 
     * We can not use the escape key in webgl because it "kills" the game and then 
     * the player would not understand why the game does not work anymore, after pressing 
     * the escape key to get out of fullscreen mode for example. 
     * 
    */
    
#if !UNITY_WEBGL
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit(); 
        }
#endif
    }

    public void OnClickPlay() {
        PlayerPrefs.SetInt("Level", 1);
        SceneManager.LoadScene("1D"); 
    }

    public void OnClickLevel() {
        SceneManager.LoadScene("Levels"); 
    }

    public void OnClickCredits() {
        SceneManager.LoadScene("Credits"); 
    }
}
