using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class PauseSprite : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
    public void SpritePause() {
        GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Pause"); 
    }

    public void SpritePlay() {
        GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Play");
    }
}
