using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (!PlayerPrefs.HasKey("Level")) {
            PlayerPrefs.SetInt("Level", 1); 
        }
	}
	
	// Update is called once per frame
	void Update () {
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
