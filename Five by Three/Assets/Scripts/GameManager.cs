using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {
	public float spawnTime, initialFallSpeed;
	public GameObject redC, greenC, yellowC;
	public GameObject[] spawnLocation;
	public float fallSpeedAdd;
	public float fallSpeed = 0f;
	// Use this for initialization
	void Start () {
		StartCoroutine (spawnCards ());
	}
	
	IEnumerator spawnCards(){
		yield return new WaitForSeconds (spawnTime);
		GameObject card;
		int l = Random.Range (0, spawnLocation.Length);
		card = Instantiate (getCard(), spawnLocation [l].transform.position, this.transform.rotation);
		card.GetComponent<Rigidbody2D> ().gravityScale = initialFallSpeed + fallSpeed;
		int k = Random.Range (0, spawnLocation.Length);
		if (l != k) {
			card = Instantiate (getCard (), spawnLocation [k].transform.position, this.transform.rotation);
			card.GetComponent<Rigidbody2D> ().gravityScale = initialFallSpeed + fallSpeed;
		} 

		StartCoroutine (spawnCards ());

	}
	private GameObject getCard(){
		int y = Random.Range (0, 12);
		if (y >= 0 && y <= 4) {
			return redC;
		} else if (y >=5 && y <= 8) {
			return greenC;
		} else {
			return yellowC;
		}
	}
	public void adjustFallSpeed(){
		fallSpeed += fallSpeedAdd;
	}

	public void resetGame(){
		SceneManager.LoadScene ("gameroom");
	}
}
