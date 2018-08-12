using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Le trou noir s'étend au fur et à mesure. 
/// </summary>
public class TrouNoir : MonoBehaviour {

    public float speed;

    private GameObject GameManagerObject;

    // Le rayon du trou noir. 
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
        UpdatePositionTrouNoir(); 
	}

    public void UpdatePositionTrouNoir() {
        if (Player == null || Stop) {
            return; 
        }
        rayon += Time.deltaTime * 3;
        float distancePlayerCenter = Player.transform.position.magnitude; 
        GameManagerObject.GetComponent<GameManager>().UpdateDistanceTrouNoir(distancePlayerCenter - rayon);
        ParticleSystem.GetComponent<ForceParticulesTrouNoir>().ChangeStrengthParticules(distancePlayerCenter - rayon); 
        if ((distancePlayerCenter - rayon <= 0) || (distancePlayerCenter - rayon >= distanceGoal)) {
            Stop = true; 
        }
    }

    public void SetDistanceGoal(int distanceGoal) {
        this.distanceGoal = distanceGoal; 
    }
}
