using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReporter : MonoBehaviour {
	public SmashableProp prop;

	void OnCollisionEnter(Collision collision) {
		float force = Mathf.Abs(Vector3.Dot(collision.relativeVelocity,collision.contacts[0].normal));
		prop.TakeDamage(force);
	}
}
