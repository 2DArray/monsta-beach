using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectReference : MonoBehaviour {

	public Collider terrainCollider;

	public static ObjectReference instance;
	void Awake () {
		instance=this;
	}
}
