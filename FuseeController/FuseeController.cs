using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Parent Controller for all rocket controllers. 
/// </summary>
public abstract class FuseeController : MonoBehaviour {

    public bool canMove = true;

    /// <summary>
    /// Fuel of the rocket
    /// </summary>
    public int Fuel;

    public virtual void Update() {
        CheckFuel(); 
    }

    /// <summary>
    /// Check if there is enough fuel left. 
    /// Everytime the player is pressing the button to move, we remove fuel. 
    /// </summary>
    private void CheckFuel() {
        if (!canMove) {
            return; 
        }
        if (GetComponent<Rigidbody>().velocity != Vector3.zero) {
            Fuel++;
        }
        if (Fuel == 300) {
            Fuel = 0;
            GameObject.Find("GameManager").GetComponent<GameManager>().RemoveFuel();
        }
    }

    /// <summary>
    /// When colliding with an object : Planet or Fuel 
    /// </summary>
    /// <param name="collision"></param>
    public void OnCollisionEnter(Collision collision) {
        if (!canMove) {
            return; 
        }
        if (collision.gameObject.tag == "Planet") {
            // If collision with a planet 
            Death();
        } else if (collision.gameObject.tag == "Carburant") {
            GameObject ExplosionCarburant = Instantiate(Resources.Load<GameObject>("ParticleCarburant"));
            ExplosionCarburant.transform.position = collision.gameObject.transform.position;
            GameObject.Find("GameManager").GetComponent<GameManager>().AddFuel();
            Destroy(collision.gameObject);
            Destroy(ExplosionCarburant, 2f); 
        }
    }

    public void OnTriggerEnter(Collider collision) {
        Debug.Log("Collider");
        if (collision.gameObject.tag == "Planet") {
            // If collision with a planet
            Death();
        }
        else if (collision.gameObject.tag == "Carburant") {
            GameObject ExplosionCarburant = Instantiate(Resources.Load<GameObject>("ParticleCarburant3D"));
            ExplosionCarburant.transform.position = collision.gameObject.transform.position;
            GameObject.Find("GameManager").GetComponent<GameManager>().AddFuel();
            Destroy(collision.gameObject);
            Destroy(ExplosionCarburant, 2f);

            if (GetComponent<AudioSource>() == null) {
                gameObject.AddComponent<AudioSource>(); 
            }
            GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Music/Carburant");
            GetComponent<AudioSource>().Play();
        }
    }

    /// <summary>
    /// All deaths are different for every type of rocket. 
    /// </summary>
    public virtual void Death() {

    }

    /// <summary>
    /// Play the music when reaching the end of a level.
    /// </summary>
    public void PlayWin() {
        if (GetComponent<AudioSource>() == null) {
            gameObject.AddComponent<AudioSource>();
        }
        GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Music/win");
        GetComponent<AudioSource>().Play();
    }

}
