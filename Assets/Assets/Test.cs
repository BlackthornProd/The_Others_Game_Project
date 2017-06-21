using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	public List<int> myInts = new List<int>(); // created a new List !

	void Start(){

		myInts[1] = 5;// Here I make the myInts of index 1 equal to 5 !
	}

	void Update(){

		if(Input.GetKeyDown(KeyCode.Space)){

			myInts.Add(Random.Range(0, 1000)); // Here we are adding a random number to our myInts list.
		}

		if(Input.GetKeyDown(KeyCode.Q)){

			myInts.Remove(myInts[Random.Range(0, myInts.Count)]); // Here we are removing a random element from my myInts list
		}

		if(Input.GetKeyDown(KeyCode.R)){
			myInts.RemoveRange(0, 5);
		}
	}
}
