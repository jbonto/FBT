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
	public enum controlScheme {Keyboard, Touch, Gyrometer};
	public controlScheme currentControls;
	public int goal;
	public float moveSpeed;
	public string levelName;
	public float screenRight;
	public float screenLeft;
	public float threshold;

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
		if (currentControls == controlScheme.Keyboard) {
			KeyboardControls ();
		} else if (currentControls == controlScheme.Touch) {
			TouchControls ();
		} else if (currentControls == controlScheme.Gyrometer) {
			GyroControls ();
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
		float playerPercent = (this.transform.position.x - screenLeft) / (screenRight - screenLeft);
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
