using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// When the player collides with a planet, 
/// the rocket his replaced by this "explosion rocket" that explodes on start, 
/// with an explosion animation. 
/// </summary>
public class ExplodeFusee : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log(GetComponent<AudioSource>()); 
        if (GetComponent<AudioSource>() != null) {
            GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Music/ExplosionFusee");
            GetComponent<AudioSource>().Play(); 
        }
		foreach(Transform t in transform) {
            if (t.GetComponent<Rigidbody>() != null) {
                t.GetComponent<Rigidbody>().AddForce(new Vector3(t.position.x - transform.position.x,
                    t.position.y - transform.position.y, t.position.z - transform.position.z) * 50f);
            }
        }
        GameObject.Find("GameManager").GetComponent<GameManager>().Lose("Your rocket exploded"); 
	}

}
