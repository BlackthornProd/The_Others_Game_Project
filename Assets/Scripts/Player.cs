using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

	[Header("Player Stats")]
	public float speed = 5f;
	public float mouseSensativity = 5f;
	public float upDownRange = 60f;
	public int will = 100;


	private float rotUpDown = 0f;

	[Header("References Player")]
	private CharacterController characterController;
	public Camera cam;
	public Text willDisplay;
	public GameObject hurtOverlay;
	public Animator overlayAnim;
	public SpawnerScript spawner;
	private WhatWave whatWave;

	void Start(){

		characterController = GetComponent<CharacterController>();
		spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<SpawnerScript>();
		Cursor.visible = false;
	}

	void Update(){

		willDisplay.text = "Will : " + will;

		if(will > 100){
			will = 100;
		}

		if(will <= 0){
			SceneManager.LoadScene("Level1");
		}


		// player movement
		float horizontalInput = Input.GetAxisRaw("Horizontal");
		float verticalInput = Input.GetAxisRaw("Vertical");
		Vector3 movement = new Vector3(horizontalInput, 0, verticalInput);
		movement = transform.rotation * movement;
		characterController.SimpleMove(movement.normalized * speed);

		// player rotation
		float rotLeftRight = Input.GetAxisRaw("Mouse X") * mouseSensativity;
		rotUpDown -= Input.GetAxisRaw("Mouse Y") * mouseSensativity;
		cam.transform.Rotate(-rotUpDown, 0, 0);
		transform.Rotate(0, rotLeftRight, 0);

		// Restrain the player from rotation too much
		rotUpDown = Mathf.Clamp(rotUpDown, -upDownRange, upDownRange);
		cam.transform.localRotation = Quaternion.Euler(rotUpDown, 0, 0);

	}

	public void TakeDamage(int damage){
		will -= damage;
		StartCoroutine(HurtOverlayDisplay());
	}

	IEnumerator HurtOverlayDisplay(){

		hurtOverlay.SetActive(true);
		overlayAnim.SetTrigger("OverlayTrigger");
		yield return new WaitForSeconds(0.2f);
		hurtOverlay.SetActive(false);
	}



}
