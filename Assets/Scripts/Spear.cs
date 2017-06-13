using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour {
	public Vector3 velocity;
	public float startLifetime;
	public AnimationCurve scaleCurve;

	AudioSource audio;
	bool stuck = false;
	Vector3 startScale;
	float lifetime;
	Vector3 testPoint;
	Transform stuckObject;
	Vector3 stuckOffset;
	Quaternion stuckRotOffset;
	void Start () {
		testPoint=Vector3.up*Random.value;
		transform.rotation=Quaternion.LookRotation(velocity)*Quaternion.Euler(90f,0f,0f);
		startScale=transform.localScale;
		lifetime=startLifetime;
		audio=GetComponent<AudioSource>();
	}
	
	void Update () {
		lifetime-=Time.deltaTime;
		transform.localScale=startScale*scaleCurve.Evaluate(lifetime/startLifetime);

		if (stuck==false) {
			Debug.DrawRay(transform.TransformPoint(testPoint),Vector3.up,Color.green);
			RaycastHit hit = new RaycastHit();
			if (Physics.Raycast(transform.TransformPoint(testPoint),velocity,out hit,velocity.magnitude*Time.deltaTime)) {
				transform.position+=velocity.normalized*hit.distance;
				stuckObject=hit.collider.transform;
				stuckOffset=stuckObject.InverseTransformPoint(transform.position);
				stuckRotOffset=Quaternion.Inverse(stuckObject.rotation)*transform.rotation;
				stuck=true;
				audio.Stop();
				Sounds.PlaySound("spear stab",transform.position);
			} else {
				transform.position+=velocity*Time.deltaTime;
			}
		} else {
			if (stuckObject!=null) {
				transform.position=stuckObject.TransformPoint(stuckOffset);
				transform.rotation=stuckObject.rotation*stuckRotOffset;
			} else {
				lifetime=0f;
			}
		}

		if (lifetime<=0f) {
			Destroy(gameObject);
		}
	}
}
