using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseeController : MonoBehaviour {

    public bool canMove = true;

    public int Carburant;

    public virtual void Update() {
        CheckCarburant(); 
    }

    private void CheckCarburant() {
        if (GetComponent<Rigidbody>().velocity != Vector3.zero) {
            Carburant++;
        }
        if (Carburant == 500) {
            Carburant = 0;
            GameObject.Find("GameManager").GetComponent<GameManager>().EnleverCarburant();
            Debug.Log("EnleverCarburant"); 
        }
    }

    public void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Planet") {
            // Si on se cogne dans une planete. 
            Mort();
        } else if (collision.gameObject.tag == "Carburant") {
            GameObject ExplosionCarburant = Instantiate(Resources.Load<GameObject>("ParticleCarburant"));
            ExplosionCarburant.transform.position = collision.gameObject.transform.position;
            GameObject.Find("GameManager").GetComponent<GameManager>().AjouterCarburant();
            Destroy(collision.gameObject);
            Destroy(ExplosionCarburant, 2f); 
        }
    }

    public void OnTriggerEnter(Collider collision) {
        Debug.Log("Collider");
        if (collision.gameObject.tag == "Planet") {
            // Si on se cogne dans une planete. 
            Mort();
        }
        else if (collision.gameObject.tag == "Carburant") {
            GameObject ExplosionCarburant = Instantiate(Resources.Load<GameObject>("ParticleCarburant3D"));
            ExplosionCarburant.transform.position = collision.gameObject.transform.position;
            GameObject.Find("GameManager").GetComponent<GameManager>().AjouterCarburant();
            Destroy(collision.gameObject);
            Destroy(ExplosionCarburant, 2f);

            if (GetComponent<AudioSource>() == null) {
                gameObject.AddComponent<AudioSource>(); 
            }
            GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Music/Carburant");
            GetComponent<AudioSource>().Play();
        }
    }

    public virtual void Mort() {

    }

    public void PlayWin() {
        if (GetComponent<AudioSource>() == null) {
            gameObject.AddComponent<AudioSource>();
        }
        GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Music/win");
        GetComponent<AudioSource>().Play();
    }

}
