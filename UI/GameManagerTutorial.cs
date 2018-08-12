using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using LanguageModule; 

public class GameManagerTutorial : MonoBehaviour {

    private int numeroTutorial;

    public int NumberSteps = 8; 

    private GameObject TextTutorial;

    private GameObject TrouNoir;

    public string fichier = "tutorialsIntro"; 

	// Use this for initialization
	void Start () {
        TrouNoir = GameObject.Find("TrouNoir");
        TrouNoir.SetActive(false); 
        TextTutorial = GameObject.Find("TutorialText"); 
        numeroTutorial = 1;
        GoToNextStepTutorial(); 
	}

    private void Update() {
        if (Input.anyKeyDown) {
            numeroTutorial += 1; 
            GoToNextStepTutorial(); 
        }
    }

    private void GoToNextStepTutorial() {
        if (numeroTutorial > NumberSteps) {
            TrouNoir.SetActive(true);
            GameObject.Find("GameManager").GetComponent<GameManager>().SetDistanceGoal(); 
            Destroy(gameObject);
        } else {
            TextTutorial.GetComponent<Text>().text = LanguageData.GetString("String" + numeroTutorial.ToString(), fichier);
        }
    }
}
