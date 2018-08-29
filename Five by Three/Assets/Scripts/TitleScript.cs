using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TitleScript : MonoBehaviour {

	public string levelName;

	public void goTo(){
		SceneManager.LoadScene (levelName);
	}

}
