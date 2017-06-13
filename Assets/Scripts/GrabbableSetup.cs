using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableSetup : MonoBehaviour {

	void Start () {
		int i;
		int layer = LayerMask.NameToLayer("Grabbable");
        GameObject[] sceneObjects = FindObjectsOfType<GameObject>();
		for (i=0;i<sceneObjects.Length;i++) {
			if (sceneObjects[i].layer==layer) {
				Rigidbody rigidbody = sceneObjects[i].GetComponent<Rigidbody>();
				if (rigidbody==null) {
					sceneObjects[i].AddComponent<Rigidbody>().isKinematic=true;
				}
			}
		}
	}
}
