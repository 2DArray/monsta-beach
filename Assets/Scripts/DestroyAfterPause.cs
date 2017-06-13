using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterPause : MonoBehaviour {
	public float pauseDuration;
	void Start () {
		Destroy(gameObject,pauseDuration);
	}
}
