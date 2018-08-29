using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
public class PlayerScript : MonoBehaviour {
	public Vector2 playerTouchPosition;
	private int red = 0, green = 0, yellow = 0;
	private Rigidbody2D RB2D;
	public int goal;
	public float moveSpeed;
	private int sets = 0;
	public Text setDisplay, redScoreDisplay, greenScoreDisplay, yellowScoreDisplay;
	public string levelName;
	public GameManager gm;
	public AudioClip cardSound;
	private AudioSource audio;
	private AudioMixer audioMixer;
	public AudioMixerGroup reg, echo;
	private Vector2 empty = new Vector2(0f,0f);
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
		float m = Input.GetAxis ("Horizontal");
		RB2D.velocity = new Vector2 (moveSpeed * m, 0f);
		if(Input.GetTouch (0).deltaPosition!=empty){
		setDisplay.text = (Input.GetTouch (0).deltaPosition).ToString();
		}
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
