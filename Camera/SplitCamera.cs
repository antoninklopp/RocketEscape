using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Camera that splits randomly in the fifth dimension. 
/// Everytime the player hits the space bar, we create a "new reality". 
/// We split the screen in two by creating a new camera with a random view, 
/// and putting the current camera in one of the two smaller screen spaces. 
/// </summary>
public class SplitCamera : MonoBehaviour {

    /*
     * Size and position of the player camera. 
     * After the creation of the new cameras, they will
     * not be split anymore. 
     * We don't keep their positions. 
     */ 
    float x = 0;
    float y = 0;
    float w = 1;
    float h = 1;

    /// <summary>
    /// Checks if the screen has to be split horizontally or vertically. 
    /// </summary>
    bool cutHorizontal = true; 

    /// <summary>
    /// Split the screen by creating a new camera. 
    /// </summary>
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
