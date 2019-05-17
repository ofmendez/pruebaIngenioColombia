using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : Singleton<EnemiesController> {

	List<GameObject> enemies ;
	public GameObject enemyPrefab;
	int enemiesToCreate;
	float enemySpeed;


	void Start () {
		enemySpeed = 0.54f;
 		enemies = new List<GameObject>();	
	}
	
	public void LaunchHorde (int _e) {
		enemiesToCreate = _e;
		InvokeRepeating("CreateEnemy",0f,0.2f);
	}

	public void CreateEnemy () {
		if (enemiesToCreate ==1)
			CancelInvoke ();
 		GameObject enemy = Instantiate (enemyPrefab);
	    enemy.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = enemySpeed;
 		enemies.Add(enemy);
 		enemiesToCreate --;
	}

	public void IncreaseEnemiesSpeed (float inc) {
		enemySpeed += enemySpeed*inc;
		foreach(GameObject e in enemies) {
		   e.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = enemySpeed;
		}
	}
}
