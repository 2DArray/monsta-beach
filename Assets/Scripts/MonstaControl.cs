using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonstaControl : MonoBehaviour {
	public Transform cam;
	public float moveForce;
	public SphereCollider bodyCollider;
	public GameObject sandPuffEffect;

	public static float moveTimer = 0f;

	[System.NonSerialized]
	new public Rigidbody rigidbody;

	public static MonstaControl instance;

	void Start () {
		rigidbody=GetComponent<Rigidbody>();
		instance=this;
		moveTimer=0f;
	}
	void FixedUpdate() {
		Vector2 inputDir = Vector2.zero;
		bool moving = false;
		if (Input.GetKey(KeyCode.UpArrow)) {
			inputDir.y+=1f;
			moving=true;
		}
		if (Input.GetKey(KeyCode.DownArrow)) {
			inputDir.y-=1f;
			moving=true;
		}
		if (Input.GetKey(KeyCode.LeftArrow)) {
			inputDir.x-=1f;
			moving=true;
		}
		if (Input.GetKey(KeyCode.RightArrow)) {
			inputDir.x+=1f;
			moving=true;
		}

		// Timer, for TitleCard.cs
		if (moving) {
			moveTimer+=Time.deltaTime;
		}

		// Constrain diagonal speed
		if (inputDir.sqrMagnitude>1f) {
			inputDir.Normalize();
		}

		// Movement is relative to the camera
		Vector3 forward = cam.forward;
		forward.y=0f;
		forward.Normalize();

		Vector3 right = cam.right;
		right.y=0f;
		right.Normalize();

		Vector3 moveDir = (forward*inputDir.y+right*inputDir.x);

		// Convert movement direction to torque
		// ("Roll the ball this way")
		rigidbody.AddTorque(Vector3.Cross(Vector3.up,moveDir)*moveForce);
	}
}
