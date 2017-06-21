using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhatWave : MonoBehaviour {

	public static WhatWave instance;

	public int waveNumber = 1;

	void Awake(){

		if(instance == null){
			instance = this;
			DontDestroyOnLoad(transform.gameObject);
		} else if(instance != this){
			Destroy(gameObject);
		}

	}

	void Update(){
		Debug.Log(waveNumber);
	}

}
