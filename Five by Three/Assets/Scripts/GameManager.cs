using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {
	public float spawnTime, initialFallSpeed;
	public GameObject ironAsteroid, crystalAsteroid, goldAsteroid;
	public GameObject[] spawnLocation;
	public float fallSpeedAdd;
	public float fallSpeed = 0f;
	// Use this for initialization
	void Start () {
		StartCoroutine (spawnCards ());
	}
	
	IEnumerator spawnCards(){
		yield return new WaitForSeconds (spawnTime);
		GameObject asteroid;
		int l = Random.Range (0, spawnLocation.Length);
		asteroid = Instantiate (getAsteroid(), spawnLocation [l].transform.position, this.transform.rotation);
		asteroid.GetComponent<Rigidbody2D> ().gravityScale = initialFallSpeed + fallSpeed;
		int k = Random.Range (0, spawnLocation.Length);
		if (l != k) {
			asteroid = Instantiate (getAsteroid (), spawnLocation [k].transform.position, this.transform.rotation);
			asteroid.GetComponent<Rigidbody2D> ().gravityScale = initialFallSpeed + fallSpeed;
		} 

		StartCoroutine (spawnCards ());

	}
	private GameObject getAsteroid(){
		int y = Random.Range (0, 12);
		if (y >= 0 && y <= 4) {
			return ironAsteroid;
		} else if (y >=5 && y <= 8) {
			return crystalAsteroid;
		} else {
			return goldAsteroid;
		}
	}
	public void adjustFallSpeed(){
		fallSpeed += fallSpeedAdd;
	}

	public void resetGame(){
		SceneManager.LoadScene ("gameroom");
	}
}
