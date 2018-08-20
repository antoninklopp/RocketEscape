using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The black hole gets bigger every second. 
/// The player must escape ! 
/// </summary>
public class TrouNoir : MonoBehaviour {

    public float speed;

    private GameObject GameManagerObject;

    // The radius of the black hole
    private float rayon;

    private GameObject Player;

    private GameObject ParticleSystem;

    public bool Stop;

    public int distanceGoal; 

	// Use this for initialization
	void Start () {
        rayon = 0;
        GameManagerObject = GameObject.Find("GameManager");
        Player = GameObject.FindGameObjectWithTag("Player");
        ParticleSystem = GameObject.Find("ParticulesTrouNoir"); 
	}
	
	// Update is called once per frame
	void Update () {
        UpdatePositionBlackHole(); 
	}

    /// <summary>
    /// Updates the position of the black hole at every frame
    /// </summary>
    public void UpdatePositionBlackHole() {
        if (Player == null || Stop) {
            return; 
        }
        rayon += Time.deltaTime * 3;
        float distancePlayerCenter = Player.transform.position.magnitude; 
        GameManagerObject.GetComponent<GameManager>().UpdateDistanceBlackHole(distancePlayerCenter - rayon);
        ParticleSystem.GetComponent<ForceParticulesTrouNoir>().ChangeStrengthParticules(distancePlayerCenter - rayon); 
        if ((distancePlayerCenter - rayon <= 0) || (distancePlayerCenter - rayon >= distanceGoal)) {
            Stop = true; 
        }
    }

    /// <summary>
    /// Set the distance to reach by the player to finish the level. 
    /// </summary>
    /// <param name="distanceGoal"></param>
    public void SetDistanceGoal(int distanceGoal) {
        this.distanceGoal = distanceGoal; 
    }
}
