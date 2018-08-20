using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Rocket controller for the second dimension. 
/// </summary>
public class FuseeController2D : FuseeController {

    public float speed;

    Rect screen;

	// Use this for initialization
	void Start () {
        canMove = true; 
        transform.Find("Boost").gameObject.SetActive(false);
        screen = new Rect(0, 0, Screen.width, Screen.height);
    }
	
	// Update is called once per frame
	public override void Update () {
        base.Update();
        Move(); 
	}

    /// <summary>
    /// Faire bouger le vaisseau
    /// </summary>
    private void Move() {

        if (!canMove) {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            return; 
        }

        Vector3 MousePosition = Input.mousePosition;
        MousePosition = Camera.main.ScreenToWorldPoint(MousePosition);

        if (Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0)) {
            // Quand on appuie sur la barre espace
            transform.Find("Boost").gameObject.SetActive(true);
        } 
        else if (Input.GetButtonUp("Jump") || Input.GetMouseButtonUp(0)) {
            transform.Find("Boost").gameObject.SetActive(false);
        }

        if (Input.GetAxisRaw("Jump") == 1 || Input.GetMouseButton(0)) {
            GetComponent<Rigidbody>().velocity = new Vector3(0, speed, 0);
        }
        else if (Input.GetAxisRaw("Jump") == 0 || !Input.GetMouseButton(0)) {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }


        if (screen.Contains(Input.mousePosition)) {
            transform.position = new Vector3(MousePosition.x, transform.position.y, transform.position.z);
        } else if (Input.mousePosition.x < 0) {
            transform.position = new Vector3(Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x, transform.position.y, transform.position.z);
        } else if (Input.mousePosition.x > Screen.width) {
            transform.position = new Vector3(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x, transform.position.y, transform.position.z);
        }
    }

    public override void Death() {
        GameObject NewFusee = Resources.Load<GameObject>("Fusee/FuseeExplode");
        GameObject InstantiatedFusee = Instantiate(NewFusee);
        InstantiatedFusee.transform.position = gameObject.transform.position;
        Destroy(gameObject); 
    }

}
