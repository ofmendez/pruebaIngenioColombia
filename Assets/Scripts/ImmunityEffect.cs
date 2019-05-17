using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmunityEffect : MonoBehaviour {
	public GameObject obj;
	
	void Update () {
		obj.SetActive(((int)(Time.time*10)) %3 ==0?!this.gameObject.activeSelf:this.gameObject.activeSelf);
	}

	public void Reset(){
		obj.SetActive(true);
	}
}
