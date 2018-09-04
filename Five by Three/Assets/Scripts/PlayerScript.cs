using System.Collections;
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
	public enum controlScheme {Keyboard, TouchHold, TouchTap, Gyrometer};
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
	// Use this for initialization
	void Start () {
		PlayerPrefs.SetInt ("sets", 0);

		setDisplay.text = "Sets: " + sets.ToString ();
		redScoreDisplay.text = "Red: " + red.ToString ();
		greenScoreDisplay.text = "Green: " + green.ToString ();
		yellowScoreDisplay.text = "Yellow: " + yellow.ToString ();
		RB2D = GetComponent<Rigidbody2D> ();
		PlayerPrefs.SetInt ("sets", 0);
		audioMixer = Resources.Load ("Audio/CardMixer") as AudioMixer;
		audio = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		

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
		}
	}

	void KeyboardControls(){
		float m = Input.GetAxis ("Horizontal");
		RB2D.velocity = new Vector2 (moveSpeed * m, 0f);
	}

	void TouchControls(){
		RB2D.velocity = new Vector2 (moveSpeed * determineMovement(Input.GetTouch (0).position.x), 0f);
		setDisplay.text = Input.GetTouch (0).deltaPosition.x.ToString ();
	}

	float determineMovement(float touchPos){
		if (Input.GetTouch (0).deltaPosition.x == 0f)
			return 0f;

		float touchPercent = touchPos / Screen.width;
		if (touchPercent >= playerPercent + threshold) {
			return 1f;
		} else if (touchPercent < playerPercent - threshold) {
			return -1f;
		} else {
			return 0f;
		}
	}
	void GyroControls(){

	}
	void TouchTap(){
		if (Input.GetTouch (0).position!=empty) {
			RB2D.velocity = new Vector2 (0f, 0f);
			setDisplay.text =isMoving.ToString();
			StopCoroutine (MoveToTap (0f));
			StartCoroutine (MoveToTap (Input.GetTouch (0).position.x));
		}
	}
	public IEnumerator MoveToTap(float touchPos){
		isMoving = true;
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
		isMoving = false;
	}
	float screenPosition(float percent){
		float i;
		i = screenLeft + ((screenRight - screenLeft) * percent);
		return i;
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
