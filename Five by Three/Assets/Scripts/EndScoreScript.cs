using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class EndScoreScript : MonoBehaviour {
	public string levelName;
	public Text scoreText;
	public int[] playerScores;
	public TextMeshProUGUI[] HSText;
	// Use this for initialization
	void Start () {
		string entry = "score";
		for (int i = 0; i < (playerScores.Length-1); i++) {
			playerScores [i] = PlayerPrefs.GetInt (entry);
			entry += i;
		}
		playerScores [playerScores.Length-1] = PlayerPrefs.GetInt ("sets");
		for(int z = 0; z < playerScores.Length; z++){
			for (int i = 0; i < (playerScores.Length - 1); i++) {
				if (playerScores [i] < playerScores [i + 1]) {
					int q = playerScores[i+1];
					playerScores [i + 1] = playerScores [i];
					playerScores [i] = q;
				}
			}
		} 
		for (int i = 0; i < playerScores.Length; i++) {
			Debug.Log (i+"   "+playerScores [i]);	
		}
		entry = "score";
		for (int i = 0; i < (playerScores.Length-1); i++) {
			try{
				HSText[i].SetText(playerScores[i].ToString());
			} catch {
				Debug.Log ("Textmesh " + i + " is nonexistant");
			}
			PlayerPrefs.SetInt(entry, playerScores[i]);
			entry += i;
		} 
		/**
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
		*/
	}
	public void goTo(){
		SceneManager.LoadScene (levelName);
	}

}
