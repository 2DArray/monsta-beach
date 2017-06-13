using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandPuff : MonoBehaviour {
	public float scaleMult = 1f;
	public bool playSounds = true;
	void Start () {
		
	}
	
	void OnCollisionEnter(Collision collision) {
		if (collision.collider.CompareTag("Sand")) {
			GameObject puff=Instantiate(MonstaControl.instance.sandPuffEffect,collision.contacts[0].point,Quaternion.LookRotation(collision.contacts[0].normal));
			puff.transform.localScale=Vector3.one*collision.relativeVelocity.magnitude*.04f*scaleMult;
			if (playSounds) {
				Sounds.PlaySound("sand slap",collision.contacts[0].point,Mathf.Clamp01(collision.relativeVelocity.magnitude/20f));
			}
		} else {
			if (playSounds) {
				Sounds.PlaySound("wood slap",collision.contacts[0].point,Mathf.Clamp01(collision.relativeVelocity.magnitude/20f));
			}
		}
	}
}
