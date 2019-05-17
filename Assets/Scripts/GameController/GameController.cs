using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {




    int actualLevel;
    float timeRemaining;

    public void Init()
    {
        actualLevel = 0;
        timeRemaining = 31;
    }
    void Update () {
        timeRemaining -= Time.deltaTime;
        HudController.main.time = timeRemaining;
        if (Input.GetKeyDown(KeyCode.P))
             PauseGame();
        
	}
	
	
	public void PauseGame () {
        Time.timeScale = 0;
		
	}
    public void ResumeGame()
    {
        Time.timeScale = 1;

    }
}
