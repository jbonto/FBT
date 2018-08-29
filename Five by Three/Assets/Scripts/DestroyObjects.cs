using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjects : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
		Debug.Log ("boop");
		Destroy (other.gameObject);
	}
}
