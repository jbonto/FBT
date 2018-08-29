using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour {
	
	public enum cardColor{red, green, yellow};
	public cardColor thisCardColor;
	private AudioSource Audio;
	public AudioClip Blip;
	public GameObject r, g, y;
	void Start(){
		Audio = GetComponent<AudioSource> ();
	}
	void OnCollisionEnter2D(Collision2D other){
		
		if (other.transform.GetComponent<PlayerScript> ()) {
			if (thisCardColor == cardColor.red) {
				other.transform.GetComponent<PlayerScript> ().addPoints (1, 0, 0);

			} else if (thisCardColor == cardColor.green) {
				other.transform.GetComponent<PlayerScript> ().addPoints (0, 1, 0);

			} else if (thisCardColor == cardColor.yellow) {
				other.transform.GetComponent<PlayerScript> ().addPoints (0, 0, 1);

			}
			Destroy (this.gameObject);
		}
	}
}
