using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceParticulesTrouNoir : MonoBehaviour {

    ParticleSystem particles; 

	// Use this for initialization
	void Start () {
        particles = transform.Find("Particle_System").gameObject.GetComponent<ParticleSystem>(); 

    }
	
	// Update is called once per frame
	void Update () {
		
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
