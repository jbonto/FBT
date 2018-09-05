using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class PrefabManager : EditorWindow{
	[MenuItem("PlayerPref/Create Window")]
	public static void ShowWindow(){
		GetWindow<PrefabManager> ("Playerpref Manager");
	}
	string fieldString, scoreID;
	void OnGUI(){
		//you must close than reopen a window to refresh the name
		fieldString = EditorGUILayout.TextField("Int to set", fieldString);
		if (GUILayout.Button ("Set the amount of completed sets")) {
			PlayerPrefs.SetInt("sets", int.Parse(fieldString));
		}
		if (GUILayout.Button ("Set the high score")) {
			PlayerPrefs.SetInt("highscore", int.Parse(fieldString));

		}
		if (GUILayout.Button ("Display playerprefs")) {
			
			Debug.Log ("The current amount of sets is: "+PlayerPrefs.GetInt ("sets"));
			Debug.Log ("The current high score is: "+PlayerPrefs.GetInt("highscore"));
		}
		if (GUILayout.Button ("Clear Playerprefs")) {
			PlayerPrefs.DeleteAll ();
		}
	}
}
