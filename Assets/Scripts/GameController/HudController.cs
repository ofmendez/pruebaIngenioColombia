using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HudController : Singleton<HudController> {

	public GameObject panelPause;
	public GameObject panelConfirm;
	public GameObject panelGameOver;

	public GameObject buttonPlay;
	public GameObject buttonPause;
	public GameObject buttonExit;
	public GameObject buttonRestart;
	
	public Text textTime;
	public Text textLevel;
	public Text textLives;
	public Text textLevelRecord;

	public Button ButtonResumeP;
	public Button ButtonResetP;
	public Button ButtonExitP;
	public Button acceptButton;
	public Button closeButton;
	public Button closeButton2;
	public Button buttonRestartGO;
	
	bool userChoose;
	string result;
    public float time;
    public bool asking;
    
    void Start () {	
		acceptButton.onClick.AddListener(()=> {
			userChoose =true;
			result ="Accept";
		});
		closeButton.onClick.AddListener(()=> {
			userChoose = true;
			result ="Close";
		});
		closeButton2.onClick.AddListener(()=> {
			closeButton.onClick.Invoke();
		});
		buttonRestartGO.onClick.AddListener(()=> {
			userChoose =true;
			result ="Accept";
		});
	}
	
	public IEnumerator AskToUser(System.Action<string> callback){
		asking=true;
		yield return new WaitUntil(() => userChoose);
		userChoose = false;
		callback(result);
		result = "";
		asking=false;
	}

	public void AskResetGame(){
		panelConfirm.gameObject.SetActive(true);
		StartCoroutine(AskToUser(result =>{
				switch (result) {
				    case "Accept":
				    	ResetGame();
				      	break;
				    case "Close":
				    	CloseConfirm();
			      		break;
				}	
		} ));
	}

	public void AskExitGame(){
		panelConfirm.gameObject.SetActive(true);
		StartCoroutine(AskToUser(result =>{
				switch (result) {
				    case "Accept":
				    	ExitGame();
				      	break;
				    case "Close":
				    	// CloseConfirm();
				      	break;
				}	
		} ));
	}

	public void AskGameOver(int _l){
		textLevelRecord.text = "Your level: "+_l;
		panelGameOver.gameObject.SetActive(true);
		StartCoroutine(AskToUser(
			result =>{ 
				if(result == "Accept")
					ExitGame();
			}  
		));
	}
	public void CloseConfirm(){
		panelConfirm.gameObject.SetActive(false);
	}

    public void ResetGame(){
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }

    public void ExitGame(){
        SceneManager.LoadScene(0);
    }

    public void SetPause( bool state) {
    	panelPause.SetActive(state);
    	buttonPause.SetActive(!state);
    	buttonExit.SetActive(!state);
    	buttonRestart.SetActive(!state);
    }

    public void SetTime( int time) {
    	textTime.text = "Next level in: "+time+"s";
    }

    public void SetLives( int lives) {
    	textLives.text = "x "+lives;
    }

    public void SetLevel( int level) {
    	textLevel.text = "Level: "+level;
    }
}
