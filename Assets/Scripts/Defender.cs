using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : MonoBehaviour {
	public GameObject deathEffect;
	public GameObject spearPrefab;
	public float maxIdleSpeed;
	public float minIdleUpwardness;
	public float minThrowTime;
	public float maxThrowTime;
	public float minThrowSpeed;
	public float maxThrowSpeed;
	public float rotateForce;
	public float deathForce;
	public float killPopForce;
	new Rigidbody rigidbody;
	Animator animator;
	float throwTimer;


	public void ThrowEvent() {
		Vector3 shoulderPos = transform.TransformPoint(Vector3.up*17f);
        GameObject spearObj = Instantiate(spearPrefab,shoulderPos,Quaternion.identity);
		Vector3 throwDir = (MonstaControl.instance.transform.position+Random.insideUnitSphere-shoulderPos).normalized;
		spearObj.GetComponent<Spear>().velocity=throwDir*Random.Range(minThrowSpeed,maxThrowSpeed);
	}

	bool HasLineOfSight() {
		Transform monsta = MonstaControl.instance.transform;

		Vector3 shoulderPos = transform.TransformPoint(Vector3.up*17f);
        Vector3 delta = monsta.position-shoulderPos;
		delta=delta.normalized*(delta.magnitude-1.5f);
		if (Physics.Raycast(shoulderPos,delta,delta.magnitude)==false) {
			return true;
		} else {
			return false;
		}
	}

	void RotateTowardMonsta() {
		Transform monsta = MonstaControl.instance.transform;
		Vector3 delta = monsta.position-transform.position;
		delta.y=0f;
		Debug.DrawRay(transform.position,delta,Color.red);
		Vector3 forward = transform.forward;
		forward.y=0f;
		Debug.DrawRay(transform.position,forward*5f,Color.blue);

		float angle = Vector3.Angle(delta,forward);
		rigidbody.MoveRotation(Quaternion.Slerp(rigidbody.rotation,Quaternion.FromToRotation(forward,delta)*rigidbody.rotation,.05f));
	}

	void Start () {
		animator=GetComponentInChildren<Animator>();
		rigidbody=GetComponent<Rigidbody>();
		throwTimer=Random.Range(minThrowTime,maxThrowTime);
	}
	
	void FixedUpdate () {
		float dot = Vector3.Dot(transform.up,Vector3.up);
		if (rigidbody.velocity.sqrMagnitude<maxIdleSpeed*maxIdleSpeed && dot>minIdleUpwardness) {
			//animator.SetBool("Fall",false);
			RotateTowardMonsta();
			if (HasLineOfSight()) {
				throwTimer-=Time.deltaTime;
				if (throwTimer<0f) {
					animator.SetTrigger("Throw");
					throwTimer=Random.Range(minThrowTime,maxThrowTime);
				}
			}
		} else {
			//animator.SetBool("Fall",true);
		}
	}
	void OnCollisionEnter(Collision collision) {
		float force = Mathf.Abs(Vector3.Dot(collision.relativeVelocity,collision.contacts[0].normal));
		if (force>deathForce) {
			if (collision.rigidbody!=null) {
				Vector3 delta = collision.collider.bounds.center-collision.contacts[0].point;
				collision.rigidbody.AddForce(delta.normalized*killPopForce,ForceMode.Impulse);
			}
			Sounds.PlaySound("kill",transform.position);
			Instantiate(deathEffect,transform.TransformPoint(Vector3.up*10.5f),Quaternion.identity);
			Destroy(gameObject);
		}
	}
}
