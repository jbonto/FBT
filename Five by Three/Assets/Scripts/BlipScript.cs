using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlipScript : MonoBehaviour {
	private Transform blipTransform;
	public float blipScale;
	private int thisisjusttogetgithubtoupdateandhaveareferencetothisscript;
	// Use this for initialization
	void Start () {
		blipTransform = this.GetComponent<Transform> ();
		if (blipScale != 0f) {
			blipTransform.localScale = new Vector3 (blipScale, blipScale, blipScale);
		}
	}
	
	public void terminate(){
		Destroy (this.gameObject);
	}
}
