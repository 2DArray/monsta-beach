using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
	public Transform target;
	public float followDistance;
	public float yOffset;
	public float rotateSpeed;
	[Range(0f,1f)]
	public float posSmooth;
	[Range(0f,1f)]
	public float rotSmooth;

	public static float turnTimer = 0f;

	float aimDir;

	Vector3 primePos;
	
	void Start () {
		Vector3 delta = MonstaControl.instance.transform.position-transform.position;
		aimDir=Mathf.Atan2(-delta.z,-delta.x);
		turnTimer=0f;
	}
	
	void Update () {

		// Old Version: no manual rotation control
		/*Vector3 delta = target.position-transform.position;
		delta.y=0f;
		Vector3 targetPos = target.position-delta.normalized*followDistance;
		targetPos+=Vector3.up*yOffset;*/

		bool turning = false;
		if (Input.GetKey(KeyCode.A)) {
			aimDir+=rotateSpeed*Time.deltaTime;
			turning=true;
		}
		if (Input.GetKey(KeyCode.D)) {
			aimDir-=rotateSpeed*Time.deltaTime;
			turning=true;
		}

		// Timer, for TitleCard.cs
		if (turning) {
			turnTimer+=Time.deltaTime;
		}

		// Convert camera angle to position offset
		Vector3 offset = new Vector3(Mathf.Cos(aimDir)*followDistance,yOffset,Mathf.Sin(aimDir)*followDistance);

		// Move camera toward intended position
		Vector3 targetPos = target.position+offset;
		transform.position=Vector3.Lerp(transform.position,targetPos,1f-Mathf.Pow(posSmooth,Time.deltaTime));

		// Point camera at target
		Quaternion targetRot = Quaternion.LookRotation(target.position-transform.position);
		transform.rotation=Quaternion.Slerp(transform.rotation,targetRot,1f-Mathf.Pow(rotSmooth,Time.deltaTime));
	}
}
