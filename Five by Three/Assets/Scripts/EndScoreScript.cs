using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class EndScoreScript : MonoBehaviour {
	public string levelName;
	public Text scoreText;
	// Use this for initialization
	void Start () {
		if (PlayerPrefs.GetInt ("sets") == PlayerPrefs.GetInt ("highscore")) {
			scoreText.text = "The current high score is: "+PlayerPrefs.GetInt("highscore").ToString()+", equal to the amount of sets you scored.";
			Debug.Log ("no new score set");
		} else if (PlayerPrefs.GetInt ("sets") > PlayerPrefs.GetInt ("highscore")) {
			scoreText.text = "Congrats!  You got a new high score of: " + PlayerPrefs.GetInt ("sets") + "!";
			Debug.Log ("new high score");
			PlayerPrefs.SetInt ("highscore", PlayerPrefs.GetInt ("sets"));
		} else if (PlayerPrefs.GetInt ("highscore") > PlayerPrefs.GetInt ("sets")) {
			scoreText.text = "The current high score is: "+PlayerPrefs.GetInt("highscore").ToString()+", no new high score has been set.";
			Debug.Log ("highscore is higher");
		}
	}
	public void goTo(){
		SceneManager.LoadScene (levelName);
	}

}
