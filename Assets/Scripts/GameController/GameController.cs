using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	// Use this for initialization
	void Update () {
        if (Input.GetKeyDown("P"))
        {
            PauseGame();
        }
	}
	
	
	public void PauseGame () {
        Time.timeScale = 0;
		
	}
}
