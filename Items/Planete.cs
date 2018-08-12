using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planete : MonoBehaviour {

    GameObject PlayerObject; 

	// Use this for initialization
	void Start () {
        PlayerObject = GameObject.FindGameObjectWithTag("Player"); 	
	}
	
	// Update is called once per frame
	void Update () {
        if (PlayerObject == null)
            return;
        if (PlayerObject.transform.position.y - transform.position.y > 50) {
            Destroy(gameObject); 
        }
        transform.position = new Vector3(transform.position.x, transform.position.y - Time.deltaTime * 2, transform.position.z); 
	}
}
