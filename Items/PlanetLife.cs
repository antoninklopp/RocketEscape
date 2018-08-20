using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// In the 4th dimension, a planet lives, grows and dies. 
/// This script controls its life and death. 
/// </summary>
public class PlanetLife : MonoBehaviour {

    public void StartDeath() {
        StartCoroutine(Death()); 
    }

    /// <summary>
    /// Death of the planet after a random time. 
    /// Respawns another planet after. 
    /// </summary>
    /// <returns></returns>
    private IEnumerator Death() {
        Vector3 InitialScale = transform.localScale;
        float factor = Random.Range(1.01f, 1.022f); 
        int WaitTime = Random.Range(3, 50);
        yield return new WaitForSeconds(WaitTime); 
        for (int i = 0; i < 130; i++) {
            yield return new WaitForSeconds(0.02f);
            transform.localScale *= factor; 
        }

        // On crée une autre planete
        GameObject newplanet = Instantiate(gameObject);
        newplanet.transform.localScale = InitialScale;
        newplanet.transform.position = new Vector3(Random.Range(-1000, 1000), Random.Range(-1000, 1000), Random.Range(-1000, 1000));
        newplanet.GetComponent<PlanetLife>().StartDeath();

        Destroy(gameObject); 
    }
	
}
