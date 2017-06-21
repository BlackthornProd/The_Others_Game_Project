using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerMaster : MonoBehaviour {

	public GameObject spawner1;
	public GameObject spawner2;
	public GameObject spawner3;
	public GameObject spawner4;

	public bool bossHit = false;

	public float headsInGame;

	public bool firstBoost;
	public bool secondBoost;
	public bool thirdBoost;

	public int timesToBeHit = 0;

	private WhatWave whatWave;


	void Start(){



		firstBoost = false;
		secondBoost = false;
		thirdBoost = false;
	}

	void Update(){

		if(headsInGame <= 59 && headsInGame >= 30){
			firstBoost = true;
			secondBoost = false;
			thirdBoost = false;
		} else if(headsInGame < 30 && headsInGame >= 13){
			firstBoost = false;
			secondBoost = true;
			thirdBoost = false;
		} else if(headsInGame < 13 && headsInGame >= 1){
			firstBoost = false; 
			secondBoost = false;
			thirdBoost = true;
		}

	}

}
