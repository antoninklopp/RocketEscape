using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

/// <summary>
/// Script to change the sprite from the play/pause 
/// button from the pause menu during the levels. 
/// </summary>
public class PauseSprite : MonoBehaviour {
	
    public void SpritePause() {
        GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Pause"); 
    }

    public void SpritePlay() {
        GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Play");
    }
}
