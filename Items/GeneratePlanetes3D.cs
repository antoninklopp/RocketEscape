using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generate planets in the 3D levels. 
/// </summary>
public class GeneratePlanetes3D : MonoBehaviour {

    /// <summary>
    /// If we are in the fourth dimension, the planet will live. 
    /// That means that they will grow and die and make another
    /// planet spawn into the universe. 
    /// </summary>
    public bool fourthDim; 

	// Use this for initialization
	void Start () {
		for (int i = 0; i < 1000; i++) {
            GenerateRandomPlanets(); 
        }

        for (int i = 0; i < 800; i++) {
            GenerateFuel(); 
        }
	}

    /// <summary>
    /// Generer une planete à une position random. 
    /// </summary>
    private void GenerateRandomPlanets() {
        GameObject[] newPlanete = Resources.LoadAll<GameObject>("Planetes3D/");
        int randomNumber = Random.Range(0, newPlanete.Length);
        GameObject Planete = Instantiate(newPlanete[randomNumber]);

        Planete.transform.SetParent(transform);

        Vector3 randomPosition = new Vector3(Random.Range(-1000, 1000), Random.Range(-1000, 1000), Random.Range(-1000, 1000));
        Planete.transform.localPosition = randomPosition;

        float randomScale = Random.Range(0.5f, 2f);
        Planete.transform.localScale *= randomScale;

        Planete.transform.SetParent(null);

        if (fourthDim) {
            Planete.AddComponent<PlanetLife>();
            Planete.GetComponent<PlanetLife>().StartDeath(); 
        }
    }

    private void GenerateFuel() {
        GameObject Carburant = Instantiate(Resources.Load<GameObject>("Hydrogene/Hydrogene3D"));

        Carburant.transform.SetParent(transform);

        Vector3 randomPosition = new Vector3(Random.Range(-1000, 1000), Random.Range(-1000, 1000), Random.Range(-1000, 1000));
        Carburant.transform.localPosition = randomPosition;

        Carburant.transform.localScale = Vector3.one * 100; 

        Carburant.transform.SetParent(null);
    }
}
