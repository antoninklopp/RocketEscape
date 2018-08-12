using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitCamera : MonoBehaviour {

    float x = 0;
    float y = 0;
    float w = 1;
    float h = 1;
    bool cutHorizontal = true; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Split() {
        GameObject newCamera = Instantiate(Resources.Load<GameObject>("Camera/BasicCamera"));
        newCamera.transform.position = new Vector3(Random.Range(-500, 500), Random.Range(-500, 500), Random.Range(-500, 500));
        newCamera.transform.eulerAngles = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)); 
        int sense = Random.Range(1, 100);
        Debug.Log(sense); 
        Debug.Log(sense % 2); 
        if (cutHorizontal) {
            w = w / 2; 
            if (sense % 2 == 1) {
                gameObject.GetComponent<Camera>().rect = new Rect(x, y, w, h); 
                newCamera.GetComponent<Camera>().rect = new Rect(x + w, y, w, h);
            } else {
                newCamera.GetComponent<Camera>().rect = new Rect(x, y, w, h);
                x = x + w;
                gameObject.GetComponent<Camera>().rect = new Rect(x, y, w, h);
            }
        } else {
            h = h / 2;
            if (sense % 2 == 1) {
                gameObject.GetComponent<Camera>().rect = new Rect(x, y, w, h);
                newCamera.GetComponent<Camera>().rect = new Rect(x, y+h, w, h);
            }
            else {
                newCamera.GetComponent<Camera>().rect = new Rect(x, y, w, h);
                y = y + h;
                gameObject.GetComponent<Camera>().rect = new Rect(x, y, w, h);
            }
        }

        cutHorizontal = !cutHorizontal; 
    }
}
