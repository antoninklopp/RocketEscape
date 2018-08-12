using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using LanguageModule; 

public class GameManager : MonoBehaviour {

    public GameObject Carburant;
    private int NombreCarburant = 4;
    private GameObject DistanceObject;

    private GameObject PlayAgainObject;
    private GameObject Menu;
    private GameObject Victory; 

    private bool GameIsPaused = false;

    private bool GoingToNextLevel = false;

    public int distanceGoal = 50;

    private string[] dimensionsString = new string[5] { "First", "Second", "Third", "Fourth", "Fifth" };

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
        DimensionInformation.GetComponent<Text>().text = dimensionsString[PlayerPrefs.GetInt("Level") - 1] + " dimension";
        SetDistanceGoal(); 
    }

    public void SetDistanceGoal() {
        if (GameObject.Find("TrouNoir") != null) {
            GameObject.Find("TrouNoir").GetComponent<TrouNoir>().SetDistanceGoal(distanceGoal);
        }
    }

    private void Update() {
        CheckForEscape(); 
    }

    public void AjouterCarburant() {
        if (NombreCarburant == 4) {
            return; 
        }
        NombreCarburant++;
        Carburant.transform.GetChild(NombreCarburant - 1).gameObject.SetActive(true); 
    }

    public void EnleverCarburant() {
        if (NombreCarburant == 1) {
            Debug.Log("Lose Carburant"); 
            Lose("You ran out of fuel"); 
        } else {
            NombreCarburant--;
            StartCoroutine(DisableCarburantAnimation()); 
        }
    }

    private IEnumerator DisableCarburantAnimation() {
        for (int i = 0; i < 5; i++) {
            if (NombreCarburant == 4) {
                yield break; 
            }
            Carburant.transform.GetChild(NombreCarburant).gameObject.SetActive(false);
            yield return new WaitForSeconds(0.15f);
            Carburant.transform.GetChild(NombreCarburant).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.15f); 
        }
        Carburant.transform.GetChild(NombreCarburant).gameObject.SetActive(false);
    }

    public void UpdateDistanceTrouNoir(float distance) {
        if (distance > distanceGoal) {
            // C'est gagné. 
            Win(); 
        }
        if (distance < 0) {
            Debug.Log("Lose distance"); 
            Lose("You have been caught by the blackhole");  
        }
        DistanceObject.GetComponent<Text>().text = ((int)distance).ToString() + "/" + distanceGoal.ToString(); 
    }

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
            GameObject.FindGameObjectWithTag("Player").GetComponent<FuseeController>().PlayWin(); 
        }
    }

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
