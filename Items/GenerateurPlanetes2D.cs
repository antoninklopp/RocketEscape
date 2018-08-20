using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 2D planet generator for the 2D level. 
/// </summary>
public class GenerateurPlanetes2D : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        transform.position = new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z);
        StartCoroutine(GeneratePlanets());
	}

    private void Update() {
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
    }

    /// <summary>
    /// Generate a random number of planets (between 1 and 3)
    /// with a random time window between to creations.
    /// </summary>
    /// <returns></returns>
    private IEnumerator GeneratePlanets() {
        while (true) {
            float WaitTime = Random.Range(0.1f, 0.4f); 
            yield return new WaitForSeconds(WaitTime);
            int type = Random.Range(0, 7);
            if (type == 0) {
                // Generer du carburant
                GenerateFuel(); 
            }
            else {
                int numberPlanets = Random.Range(1, 3);
                for (int i = 0; i < numberPlanets; i++)
                    GenerateRandomPlanet(i, numberPlanets);
            }
        }
    }

    /// <summary>
    /// Generer une planete à une position random. 
    /// </summary>
    private void GenerateRandomPlanet(int number, int total) {
        // A random planet is generated from the resources folder. 
        // By using this method, we can easily change the planets we want to generate
        // without changing anything in the code or in the editor. 
        GameObject[] newPlanete = Resources.LoadAll<GameObject>("Planetes/");
        int randomNumber = Random.Range(0, newPlanete.Length);
        GameObject Planete = Instantiate(newPlanete[randomNumber]);

        Planete.transform.SetParent(transform);

        float screenLength = Camera.main.ScreenToWorldPoint(new Vector3(
            Screen.width, 0, 0)).x;

        float minPosition = -screenLength + (number)/total * (screenLength * 2) + 1;
        float maxPosition = -screenLength + (number + 1) / total * (screenLength * 2) - 1;

        Vector3 randomPosition = new Vector3(Random.Range(minPosition, maxPosition), 0, 0);
        Planete.transform.localPosition = randomPosition;

        float randomScale = Random.Range(0.5f, 2f);
        Planete.transform.localScale *= randomScale; 

        Planete.transform.SetParent(null); 
    }
    
    /// <summary>
    /// Generate fuel randomly to allow the player to reach the end of the level. 
    /// </summary>
    private void GenerateFuel() {
        GameObject Carburant = Instantiate(Resources.Load<GameObject>("Hydrogene/Hydrogene"));

        Carburant.transform.SetParent(transform);

        float screenLength = Camera.main.ScreenToWorldPoint(new Vector3(
            Screen.width, 0, 0)).x;

        float minPosition = -screenLength + 1;
        float maxPosition = screenLength - 1;

        Vector3 randomPosition = new Vector3(Random.Range(minPosition, maxPosition), 0, 0);
        Carburant.transform.localPosition = randomPosition;

        Carburant.transform.SetParent(null);
    }
}
