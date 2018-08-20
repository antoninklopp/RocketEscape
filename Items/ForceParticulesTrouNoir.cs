using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Strength of the particle effect of the black hole. 
/// Visual information to show the player if he is close or not to the black hole. 
/// We create more particles if the player is closest to the black hole. 
/// </summary>
public class ForceParticulesTrouNoir : MonoBehaviour {

    ParticleSystem particles; 

	// Use this for initialization
	void Start () {
        particles = transform.Find("Particle_System").gameObject.GetComponent<ParticleSystem>(); 

    }

    public void ChangeStrengthParticules(float Meters) {
        var em = particles.emission;
        if (Meters > 0) {
            em.rateOverTime = (int)(1000 / Meters);
        }
        else if (Meters <= 0) {
            Debug.Log("ici"); 
            em.rateOverTime = 1000;
        }
    }
}
