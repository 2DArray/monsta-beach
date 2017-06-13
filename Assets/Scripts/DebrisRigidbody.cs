using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisRigidbody : MonoBehaviour {
	new Rigidbody rigidbody;
	void Start () {
		rigidbody=GetComponent<Rigidbody>();
	}
	
	void Update () {
		if (rigidbody.IsSleeping()) {
			Destroy(rigidbody);
			enabled=false;
		} else if (transform.position.y<-200f) {
			Destroy(gameObject);
		}
	}
}
