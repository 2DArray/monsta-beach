using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyeball : MonoBehaviour {
	public float minLookTime;
	public float maxLookTime;
	[Range(0f,1f)]
	public float turnSmooth;

	float lookTimer;
	Vector3 lookTarget;

	Vector3 defaultLocalForward;

	void Start () {
		defaultLocalForward=transform.parent.InverseTransformDirection(transform.forward);
	}
	
	void Update () {
		lookTimer-=Time.deltaTime;

		// Is our eye rolling back into our head?
		bool extremeAngle = false;
		float lookDot = Vector3.Dot((lookTarget-transform.position).normalized,(transform.parent.TransformDirection(defaultLocalForward)).normalized);
		if (lookDot<0f) {
			extremeAngle=true;
		}

		if (lookTimer<0f || extremeAngle) {
			// Pick new nearby look target (a position in world space)
			lookTimer=Random.Range(minLookTime,maxLookTime);
			Vector3 offset= Random.insideUnitSphere*10f;
			offset.y=Mathf.Abs(offset.y);
			lookTarget=transform.position+transform.parent.TransformDirection(defaultLocalForward)*10f+offset;
		}
		Quaternion targetRot=Quaternion.LookRotation(lookTarget-transform.position);
		transform.rotation=Quaternion.Slerp(transform.rotation,targetRot,1f-Mathf.Pow(turnSmooth,Time.deltaTime));
	}
}
