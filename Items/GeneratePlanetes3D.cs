using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePlanetes3D : MonoBehaviour {

    public bool fourhtDim; 

	// Use this for initialization
	void Start () {
		for (int i = 0; i < 500; i++) {
            GenererPlaneteRandom(); 
        }

        for (int i = 0; i < 200; i++) {
            GenererCarburant(); 
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// Generer une planete à une position random. 
    /// </summary>
    private void GenererPlaneteRandom() {
        GameObject[] newPlanete = Resources.LoadAll<GameObject>("Planetes3D/");
        int randomNumber = Random.Range(0, newPlanete.Length);
        GameObject Planete = Instantiate(newPlanete[randomNumber]);

        Planete.transform.SetParent(transform);

        Vector3 randomPosition = new Vector3(Random.Range(-1000, 1000), Random.Range(-1000, 1000), Random.Range(-1000, 1000));
        Planete.transform.localPosition = randomPosition;

        float randomScale = Random.Range(0.5f, 2f);
        Planete.transform.localScale *= randomScale;

        Planete.transform.SetParent(null);

        if (fourhtDim) {
            Planete.AddComponent<PlanetLife>();
            Planete.GetComponent<PlanetLife>().StartDeath(); 
        }
    }

    private void GenererCarburant() {
        GameObject Carburant = Instantiate(Resources.Load<GameObject>("Hydrogene/Hydrogene3D"));

        Carburant.transform.SetParent(transform);

        Vector3 randomPosition = new Vector3(Random.Range(-1000, 1000), Random.Range(-1000, 1000), Random.Range(-1000, 1000));
        Carburant.transform.localPosition = randomPosition;

        Carburant.transform.localScale = Vector3.one * 100; 

        Carburant.transform.SetParent(null);
    }
}
