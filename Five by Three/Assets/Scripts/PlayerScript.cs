﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
public class PlayerScript : MonoBehaviour {
	[Header("Stats")]
	public Vector2 playerTouchPosition;
	private int red = 0, green = 0, yellow = 0;
	private Rigidbody2D RB2D;
	public enum controlScheme {Keyboard, TouchHold, TouchTap, Gyrometer, Mouse};
	public controlScheme currentControls;
	public int goal;
	public float moveSpeed;
	public string levelName;
	public float screenRight;
	public float screenLeft;
	public float threshold;
	private float playerPercent;
	private bool isMoving = false;

	[Header("Referenced Objects")]
	public Text setDisplay;
	public Text redScoreDisplay;
	public Text greenScoreDisplay;
	public Text yellowScoreDisplay;
	public GameManager gm;
	public AudioClip cardSound;
	private AudioSource audio;
	private AudioMixer audioMixer;
	public AudioMixerGroup reg, echo;
	private Vector2 empty = new Vector2(0f,0f);
	private Touch playerTouch;
	private int sets = 0;
	public GameObject posTest;
	private Vector3 mousePos;
	private Vector3 h;
	// Use this for initialization
	void Start () {
		Vector3 h = this.transform.position;
		mousePos = this.transform.position;
		string test = "test";
		test += 1;
		Debug.Log (test);
		test +=1 ;
		Debug.Log (test);
		PlayerPrefs.SetInt ("sets", 0);
		posTest.transform.position = this.transform.position;
		setDisplay.text = "Sets: " + sets.ToString ();
		redScoreDisplay.text = "Red: " + red.ToString ();
		greenScoreDisplay.text = "Green: " + green.ToString ();
		yellowScoreDisplay.text = "Yellow: " + yellow.ToString ();
		RB2D = GetComponent<Rigidbody2D> ();
		PlayerPrefs.SetInt ("sets", 0);
		audioMixer = Resources.Load ("Audio/CardMixer") as AudioMixer;
		audio = GetComponent<AudioSource> ();
	}

	void FixedUpdate(){
		playerPercent = (this.transform.position.x - screenLeft) / (screenRight - screenLeft);
		if (currentControls == controlScheme.Keyboard) {
			KeyboardControls ();
		} else if (currentControls == controlScheme.TouchHold) {
			TouchControls ();
		} else if (currentControls == controlScheme.Gyrometer) {
			GyroControls ();
		} else if (currentControls == controlScheme.TouchTap) {
			TouchTap ();
		} else if (currentControls == controlScheme.Mouse) {
			MouseControls ();
		}
	}

	void KeyboardControls(){
		float m = Input.GetAxis ("Horizontal");
		RB2D.velocity = new Vector2 (moveSpeed * m, 0f);
//transform.position = Vector3.MoveTowards (transform.position, posTest.transform.position, (moveSpeed * Time.deltaTime));

	}

	void TouchControls(){
		if (Input.touchCount > 0) {
			RB2D.velocity = new Vector2 (moveSpeed * determineMovement (Input.GetTouch (0).position.x), 0f);
		} else {
			RB2D.velocity = new Vector2 (0f, 0f);
		}
//		setDisplay.text = Input.GetTouch (0).deltaPosition.x.ToString ();
	}

	float determineMovement(float touchPos){
		float touchPercent = touchPos / Screen.width;
		if (touchPercent > playerPercent + .01f) {
			return 1f;
		} else if (touchPercent < playerPercent - .01f) {
			return -1f;
		} 
		return 0f;
	}
	void GyroControls(){

	}

	void TouchTap(){
		if (Input.touchCount > 0) {
			setDisplay.text =isMoving.ToString();
			newPos(Input.GetTouch (0).position.x);
		}
		transform.position = Vector2.MoveTowards (this.transform.position, posTest.transform.position, moveSpeed * Time.deltaTime);
	}
	void newPos(float touchPos){
		float touchPercent = touchPos / Screen.width;
		posTest.transform.position = new Vector2 (screenPosition(touchPercent), this.transform.position.y);
	}
	public IEnumerator MoveToTap(float touchPos){
		int movement = -1;
		/**1 = moving to the right, 2 to the left, 3 stop
		 * 
		 * */
		float touchPercent = touchPos / Screen.width;
		if (touchPercent >= playerPercent) {
			movement = 1;
		} else {
			movement = 2;
		}
		posTest.transform.position = new Vector2 (screenPosition(touchPercent), this.transform.position.y);
		while (movement != 3) {
			yield return null;
			if (movement == 1) {
				RB2D.velocity = new Vector2 (moveSpeed, 0f);
				if(playerPercent > touchPercent-threshold){
					movement = 3;
					isMoving = false;
				}
			}
			if (movement == 2) {
				RB2D.velocity = new Vector2 (moveSpeed * -1f, 0f);
				if (playerPercent < touchPercent+threshold) {
					movement = 3;
					isMoving = false;
				}
			}
		}
		RB2D.velocity = new Vector2 (0f, 0f);
	}
	float screenPosition(float percent){
		float i;
		i = screenLeft + ((screenRight - screenLeft) * percent);
		return i;
	}

	void MouseControls(){
		
		if (Input.GetButtonDown("Fire1")) {
			h = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Debug.Log  (h.x);
			changeMous (h);
		}
		//		transform.position = Vector2.MoveTowards (this.transform.position, posTest.transform.position, moveSpeed * Time.deltaTime);
		transform.position = Vector2.MoveTowards (this.transform.position,mousePos, moveSpeed * Time.deltaTime);
	}
	void changeMous(Vector3 h){
		mousePos = new Vector3 (h.x, mousePos.y, mousePos.z);
	}
	public void addPoints(int r, int g, int y){
		red += r;
		green += g;
		yellow += y;
		//audio.outputAudioMixerGroup = echo;

		if (red > goal || green > goal || yellow > goal) {
			PlayerPrefs.SetInt ("sets", sets);
			SceneManager.LoadScene (levelName);
		} else if (red == goal && green == goal && yellow == goal) {
			red = 0;
			green = 0;
			yellow = 0;
			sets++;
			gm.adjustFallSpeed ();
			audio.outputAudioMixerGroup = echo;
			audio.PlayOneShot (cardSound);
		} else {
			audio.outputAudioMixerGroup = reg;
			audio.PlayOneShot (cardSound);
		}
		//setDisplay.text = "Sets: " + sets.ToString () + "    " + playerTouchPosition.ToString ();
		redScoreDisplay.text = "Red: " + red.ToString ();
		greenScoreDisplay.text = "Green: " + green.ToString ();
		yellowScoreDisplay.text = "Yellow: " + yellow.ToString ();
	}
}
