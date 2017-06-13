using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonstaSetup : MonoBehaviour {
	public Transform[] shoulders;
	public Collider bodyCollider;

	// Recursively add components to an arm (starting at the shoulder)
	// "depth" is the recursion depth
	void AddBoneRigidbody(Transform target,int depth) {
		Rigidbody rigidbody = target.gameObject.AddComponent<Rigidbody>();
		rigidbody.interpolation=RigidbodyInterpolation.Interpolate;
		rigidbody.mass=.25f;
		Rigidbody parentRigidbody = target.parent.GetComponentInParent<Rigidbody>();

		Joint joint = null;
		if (depth==1) {
			Physics.IgnoreCollision(target.GetComponent<Collider>(),bodyCollider);
		}
		if (depth==0) {
			joint = target.gameObject.AddComponent<FixedJoint>();
		} else {
			joint = target.gameObject.AddComponent<CharacterJoint>();
			SoftJointLimit limit = new SoftJointLimit();
			limit.limit=15f;
			SoftJointLimitSpring spring = new SoftJointLimitSpring();
			spring.spring=100f;
			spring.damper=2f;
			CharacterJoint cJoint = (CharacterJoint)joint;
			cJoint.swing1Limit=limit;
			cJoint.swing2Limit=limit;
			cJoint.highTwistLimit=limit;
			cJoint.lowTwistLimit=limit;
			cJoint.swingLimitSpring=spring;
			cJoint.twistLimitSpring=spring;
		}
		// Connect our joint to our parent
		joint.anchor=target.InverseTransformPoint(parentRigidbody.transform.position);
		joint.connectedBody=parentRigidbody;

		// if we have any children, call this function for them
		if (target.childCount>0) {
			for (int i=0;i<target.childCount;i++) {
				AddBoneRigidbody(target.GetChild(i),depth+1);
			}
		} else {
			// if we have no children, add hand components
			target.gameObject.AddComponent<Grabber>();
			target.gameObject.AddComponent<SandPuff>();
		}
	}

	void Start () {
		int i;

		gameObject.AddComponent<SandPuff>().scaleMult=2f;

		// recursively add rigidbodies to arms
		for (i=0;i<shoulders.Length;i++) {
			AddBoneRigidbody(shoulders[i],0);
		}
	}
}
