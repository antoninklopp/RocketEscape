using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseeController3D : FuseeController {

    public float speed;

    public float speedRotation;

    // Use this for initialization
    void Start() {
        canMove = true; 
        transform.Find("Boost").gameObject.SetActive(false);
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

        if (!canMove) {
            return; 
        }

        Vector3 MousePosition = Input.mousePosition;
        MousePosition = Camera.main.ScreenToWorldPoint(MousePosition);

        transform.Rotate(new Vector3(Input.mousePosition.y - Screen.height / 2, Input.mousePosition.x - Screen.width / 2, 0) * Time.deltaTime * speedRotation);
        // Debug.Log(new Vector3(Input.mousePosition.y - Screen.height / 2, Input.mousePosition.x - Screen.width/2, 0) * Time.deltaTime * speedRotation);

        if (Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0)) {
            // Quand on appuie sur la barre espace
            transform.Find("Boost").gameObject.SetActive(true);
        }
        else if (Input.GetButtonUp("Jump") || Input.GetMouseButtonUp(0)) {
            transform.Find("Boost").gameObject.SetActive(false);
        }

        if (Input.GetAxisRaw("Jump") == 1 || Input.GetMouseButton(0)) {
            GetComponent<Rigidbody>().velocity = transform.Find("Core").forward * speed;
        }
        else if (Input.GetAxisRaw("Jump") == 0 || !Input.GetMouseButton(0)) {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    public override void Mort() {
        GameObject NewFusee = Resources.Load<GameObject>("Fusee/FuseeExplode");
        GameObject InstantiatedFusee = Instantiate(NewFusee);
        InstantiatedFusee.transform.localScale = transform.localScale; 
        InstantiatedFusee.transform.position = gameObject.transform.position;
        Debug.Log(transform.Find("Camera")); 
        transform.Find("Camera").SetParent(InstantiatedFusee.transform); 
        Destroy(gameObject);
    }

}
