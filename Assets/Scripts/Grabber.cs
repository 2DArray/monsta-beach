using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour {
	public MonstaControl monsta;

	// This is a joint that sticks us to a grabbable object:
	FixedJoint joint;

	float jointTimer = 0f;
	GameObject grabObject;

	// Which direction from our body to this hand?
	Vector3 handDir {
		get {
			return (transform.position-monsta.transform.position).normalized;
		}
	}

	void Start () {
		monsta=GetComponentInParent<MonstaControl>();
	}

	void Update() {
		if (joint!=null) {
			// We're holding something - do we need to let go?
			bool releasing = false;
			if (grabObject.activeSelf==false) {
				// Let go:  Grabbed object has been disabled (ie, holding a tower when it smashes)
				Destroy(joint);
				releasing=true;
			} else {
				// Still holding onto something...
				Debug.DrawRay(transform.TransformPoint(-Vector3.right*.5f),Vector3.up,Color.red);


				if (monsta.rigidbody.velocity.magnitude>.5f) {
					Vector3 velocityDir = monsta.rigidbody.velocity.normalized;
					if (Vector3.Dot(velocityDir,handDir)<0f) {
						// Let go:  The monsta has moved past this hand
						Destroy(joint);
						releasing=true;
					}
				} else {
					// Let go:  We've lost too much speed
					// (Don't hold onto the ground while sitting still)
					Destroy(joint);
					releasing=true;
				}
			}
			if (releasing) {
				Sounds.PlaySound("release",transform.position);
			}
		}
	}
	void FixedUpdate() {
		if (joint!=null) {
			// We're holding onto something
			if (handDir.y>0f) {
				// We're grabbing something above our head - pull our body toward this hand
				monsta.rigidbody.AddForce(handDir*200f);
				if (joint.connectedBody!=null) {
					if (joint.connectedBody.isKinematic==false) {
						// We're holding a moving prop - pull it toward us
						joint.connectedBody.AddForce(-handDir*200f);
					}
				}
			}
		}
	}
	
	void OnCollisionEnter (Collision collision) {
		if (joint==null) {
			// We've hit something, and we're not already holding anything
			if (collision.collider.gameObject.layer==LayerMask.NameToLayer("Grabbable")) {
				Debug.DrawRay(collision.contacts[0].point,Vector3.up,Color.white);
				Vector3 velocityDir = monsta.rigidbody.velocity.normalized;

				// Only grab stuff if it's ahead of us (relative to our body's movement direction)
				if (Vector3.Dot(velocityDir,handDir)>0f) {
					// Stick to something
					joint=gameObject.AddComponent<FixedJoint>();
					Rigidbody otherBody = collision.gameObject.GetComponent<Rigidbody>();
					if (otherBody!=null) {
						// If it's a moving prop, connect our fixedjoint to it
						joint.connectedBody=otherBody;
						// (Otherwise, magically attach the joint to the air, since the prop will not move)
					}

					// Remember what we're grabbing, in case it disables
					grabObject=collision.gameObject;
					Sounds.PlaySound("suction",collision.contacts[0].point);
				}
			}
		}
	}
}
