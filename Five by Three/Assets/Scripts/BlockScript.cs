using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour {
	
	public enum asteroidType{Iron, Crystal, Gold};
	public asteroidType thisAsteroid;
	private AudioSource Audio;
	public AudioClip Blip;
	public GameObject trail, trail2;
	public bool randomizeStance;

	void Start(){
		Audio = GetComponent<AudioSource> ();

		if (trail) {
			trail.GetComponent<SpriteRenderer> ().sortingOrder = 
				(this.GetComponent<SpriteRenderer> ().sortingOrder - 1);

		}
		if (trail2) {
			trail2.GetComponent<SpriteRenderer> ().sortingOrder = 
				(this.GetComponent<SpriteRenderer> ().sortingOrder - 2);

		}
	}
	void OnTriggerEnter2D(Collider2D other){
		Debug.Log ("enter");
		if (other.transform.tag=="Player"  || other.GetComponent<PlayerScript>()) {
			if (thisAsteroid == asteroidType.Iron) {
				other.transform.GetComponent<PlayerScript> ().addPoints (1, 0, 0);

			} else if (thisAsteroid == asteroidType.Crystal) {
				other.transform.GetComponent<PlayerScript> ().addPoints (0, 1, 0);

			} else if (thisAsteroid == asteroidType.Gold) {
				other.transform.GetComponent<PlayerScript> ().addPoints (0, 0, 1);

			}
			Debug.Log ("detected player");
			Destroy (this.gameObject);
		}
	}
}
