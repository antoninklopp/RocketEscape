using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseeController1D : FuseeController {

    public float speed = 10f; 

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    public override void Update() {
        base.Update();
        Move();
    }

    /// <summary>
    /// Faire bouger le vaisseau
    /// </summary>
    private void Move() {

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
    }
}
