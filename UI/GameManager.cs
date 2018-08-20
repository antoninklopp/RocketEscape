using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using LanguageModule; 

/// <summary>
/// Game manager and UI of the game. 
/// </summary>
public class GameManager : MonoBehaviour {

    /*
     * UI Elements to show various informations to the player. 
     */
    public GameObject Carburant;
    private GameObject DistanceObject;
    private GameObject PlayAgainObject;
    private GameObject Menu;
    private GameObject Victory;

    private int FuelNumber = 4;

    private bool GameIsPaused = false;

    private bool GoingToNextLevel = false;

    public int distanceGoal = 50;

    private string[] dimensionsString = new string[5] { "First", "Second", "Third", "Fourth", "Fifth" };

    /// <summary>
    /// Text UI to show the dimension to the player for a better understanding. 
    /// </summary>
    private GameObject DimensionInformation;

    private int maxLevel = 5; 

	// Use this for initialization
	void Start () {
        Carburant = GameObject.Find("Fond_Carburant");
        DistanceObject = GameObject.Find("Meters");
        PlayAgainObject = GameObject.Find("PlayAgain");
        PlayAgainObject.SetActive(false);
        Menu = GameObject.Find("Menu");
        Menu.SetActive(false);
        Victory = GameObject.Find("Victory");
        Victory.SetActive(false);
        DimensionInformation = GameObject.Find("DimensionNumber");
        SetDistanceGoal();
        DimensionInformation.GetComponent<Text>().text = dimensionsString[PlayerPrefs.GetInt("Level") - 1] + " dimension";
    }

    public void SetDistanceGoal() {
        if (GameObject.Find("TrouNoir") != null) {
            GameObject.Find("TrouNoir").GetComponent<TrouNoir>().SetDistanceGoal(distanceGoal);
        }
    }

    private void Update() {
        CheckForEscape(); 
    }

    /// <summary>
    /// Add fuel when colliding with a fuel object. 
    /// </summary>
    public void AddFuel() {
        if (FuelNumber == 4) {
            return; 
        }
        FuelNumber++;
        Carburant.transform.GetChild(FuelNumber - 1).gameObject.SetActive(true); 
    }

    /// <summary>
    /// Remove Fuel on the UI. 
    /// called by the <see cref="FuseeController.CheckFuel"/>
    /// </summary>
    public void RemoveFuel() {
        if (FuelNumber == 1) {
            Debug.Log("Lose Carburant"); 
            // Informing the player about the reason of death. 
            Lose("You ran out of fuel\nLook for the H2 bottles"); 
        } else {
            FuelNumber--;
            StartCoroutine(DisableCarburantAnimation()); 
        }
    }

    /// <summary>
    /// Make the fuel icon flash to show the player he lost a fuel. 
    /// </summary>
    /// <returns></returns>
    private IEnumerator DisableCarburantAnimation() {
        for (int i = 0; i < 5; i++) {
            if (FuelNumber == 4) {
                yield break; 
            }
            Carburant.transform.GetChild(FuelNumber).gameObject.SetActive(false);
            yield return new WaitForSeconds(0.15f);
            Carburant.transform.GetChild(FuelNumber).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.15f); 
        }
        Carburant.transform.GetChild(FuelNumber).gameObject.SetActive(false);
    }

    /// <summary>
    /// Update the distance of the player to the black hole in the UI. 
    /// </summary>
    /// <param name="distance"></param>
    public void UpdateDistanceBlackHole(float distance) {
        if (distance > distanceGoal) {
            // C'est gagné. 
            Win(); 
        }
        if (distance < 0) {
            Debug.Log("Lose distance"); 
            Lose("You have been caught by the black hole");  
        }
        DistanceObject.GetComponent<Text>().text = ((int)distance).ToString() + "/" + distanceGoal.ToString(); 
    }

    /// <summary>
    /// Check for the escape key, to pause or unpause the game. 
    /// </summary>
    private void CheckForEscape() {
        if (GoingToNextLevel) {
            return; 
        }
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Tab)) {
            PauseUnpause(); 
        }
    }

    public void PauseUnpause() {
        if (GameIsPaused) {
            UnPause();
        }
        else {
            // Show the menu
            PauseGame();
        }
        GameIsPaused = !GameIsPaused;
    }

    /// <summary>
    /// Pause the game, by freezing time. 
    /// </summary>
    public void PauseGame() {
        Time.timeScale = 0; // freeze time
        Menu.SetActive(true);
        GameObject.Find("Pause").GetComponent<PauseSprite>().SpritePlay();
        GameObject.FindGameObjectWithTag("Player").GetComponent<FuseeController>().canMove = false;
        Menu.transform.Find("ReminderControls").gameObject.GetComponent<Text>().text = 
            LanguageData.GetString("tutorial" + SceneManager.GetActiveScene().name, "tutorials"); 
    }

    public void UnPause() {
        Time.timeScale = 1;
        Menu.SetActive(false);
        GameObject.Find("Pause").GetComponent<PauseSprite>().SpritePause();
        GameObject.FindGameObjectWithTag("Player").GetComponent<FuseeController>().canMove = true;
    }

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }

    public void GoToMenu() {
        SceneManager.LoadScene("Menu"); 
    }

    public void Lose(string message) {
        PlayAgainObject.SetActive(true);
        GameObject.FindGameObjectWithTag("Player").GetComponent<FuseeController>().canMove = false;
        PlayAgainObject.GetComponent<Text>().text = message; 
    }

    public void Win() {
        if (!GoingToNextLevel) {
            GoingToNextLevel = true;
            Victory.SetActive(true);
            GameObject.FindGameObjectWithTag("Player").GetComponent<FuseeController>().canMove = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<FuseeController>().PlayWin(); 
        }
    }

    /// <summary>
    /// Go to the next level. 
    /// </summary>
    public void GoToNextDimension() {
        if (PlayerPrefs.GetInt("Level") == maxLevel) {
            SceneManager.LoadScene("Credits");
        }
        else {
            PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
            SceneManager.LoadScene(PlayerPrefs.GetInt("Level").ToString() + "D");
        }
    }
}
