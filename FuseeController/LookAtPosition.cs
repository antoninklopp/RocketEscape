using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPosition : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.localPosition = new Vector3(0, Camera.main.ScreenToWorldPoint(new Vector3(0, 
            Screen.height/(2 * transform.parent.localScale.y), 0)).y - 7, 0);
	}
}
