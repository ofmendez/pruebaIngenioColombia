using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    int actualLevel;
    int lives;
    float timeRemaining;
    IEnumerator coroutine;
    
    bool paused;
    bool immune;
    bool gameOver;

    int durLevel = 10;
    int amountHorde = 3;
    float incSpeed = 0.2f;

    void Start () { 
        Init();
        HudController.main.buttonPause.GetComponent<Button>().onClick.AddListener(()=> {
            TogglePause();
        });    
        HudController.main.buttonRestart.GetComponent<Button>().onClick.AddListener(()=> {
            AskResetGame();
        }); 
        HudController.main.buttonExit.GetComponent<Button>().onClick.AddListener(()=> {
            AskExitGame();
        });  
        HudController.main.ButtonResumeP.GetComponent<Button>().onClick.AddListener(()=> {
            TogglePause();
        });    
        HudController.main.ButtonResetP.GetComponent<Button>().onClick.AddListener(()=> {
            AskResetGame();
        }); 
        HudController.main.ButtonExitP.GetComponent<Button>().onClick.AddListener(()=> {
            AskExitGame();
        });    
    }
    
    void Init(){
        paused = false;
        immune = false;
        gameOver = false;
        actualLevel = 1;
        timeRemaining = durLevel+1;
        lives = 3;
        EnemiesController.main.LaunchHorde(amountHorde);
    }

    void Update () {
        Time.timeScale = paused || HudController.main.asking ?0:1;
        timeRemaining -= Time.deltaTime;
        UpdateHud();

        if (Input.GetKeyDown(KeyCode.P) && !HudController.main.asking)
            TogglePause();
        if (Input.GetKeyDown(KeyCode.R) && !gameOver)
            AskResetGame();
        if (Input.GetKeyDown(KeyCode.Escape) && !gameOver )
            ProcessEscape();
        if (Input.GetKeyDown(KeyCode.E) && paused  )
            AskExitGame();
        if (Input.GetKeyDown(KeyCode.Return) && HudController.main.asking){
            HudController.main.acceptButton.onClick.Invoke();
            HudController.main.buttonRestartGO.onClick.Invoke();
        }

        if (timeRemaining <1)
            NextLevel();

        // Debug.Log("log");
	}



    void ProcessEscape () {
        if(HudController.main.asking){
            HudController.main.closeButton.onClick.Invoke();
        } else if (!paused){
            AskExitGame();
        }       
    }

    void NextLevel () {
        actualLevel +=1;
        timeRemaining = durLevel+1;
        EnemiesController.main.LaunchHorde(amountHorde);
        EnemiesController.main.IncreaseEnemiesSpeed(incSpeed);;
    }

    void GameOver () {
        lives --;
        gameOver = true;
        Time.timeScale = 0;
        HudController.main.AskGameOver(actualLevel); 
    }

    void UpdateHud () {
        HudController.main.SetTime ((int)timeRemaining);
        HudController.main.SetLives (lives);
        HudController.main.SetLevel (actualLevel);
    }

    IEnumerator WaitAndLaunchProjectile(float wait) {
        yield return new WaitForSeconds(wait);

    }      

    IEnumerator LostImmunity(float waitTime)    {
        this.GetComponent<ImmunityEffect>().enabled = true;
        yield return new WaitForSeconds(waitTime);
        this.GetComponent<ImmunityEffect>().enabled = false;
        this.GetComponent<ImmunityEffect>().Reset();
        immune = false;
    }

    void OnTriggerEnter(Collider other) {
        if(other.tag == "Enemy"){
            HitPlayer();
        }
    }

    public void HitPlayer (){
        if( !immune && lives>1){
            lives --;
            immune = true;
            coroutine = LostImmunity(2.0f);
            StartCoroutine(coroutine);
        }else if (!immune && lives ==1){
            GameOver(); 
        }else{

        }
    }

	public void TogglePause () {
        paused = !paused;
        HudController.main.SetPause(paused);
	}

    public void AskResetGame(){
        Time.timeScale = 0;
        HudController.main.AskResetGame(); 
    }

    public void AskExitGame(){
        Time.timeScale = 0;
        HudController.main.AskExitGame(); 
    }



}
