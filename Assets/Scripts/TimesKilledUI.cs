using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimesKilledUI : MonoBehaviour {

	public Text timesKilledDisplay;

	private WhatWave whatWave;

	void Start(){

		whatWave = GameObject.FindGameObjectWithTag("What Wave").GetComponent<WhatWave>();;
	}

	void Update(){

		timesKilledDisplay.text = whatWave.timesKilled.ToString();
	}
}
