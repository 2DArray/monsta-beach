using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashableProp : MonoBehaviour {
	public List<GameObject> fractureList;
	public GameObject staticObject;
	public float minForceForDamage = 10f;
	public float health = 100f;
	public bool isStone = false;

	new Rigidbody rigidbody;
	Rigidbody[] rigidbodies;

	void Start() {
		int i;

		// Find chunks
		fractureList = new List<GameObject>();
		for (i=0;i<transform.childCount;i++) {
			Transform child = transform.GetChild(i);
			if (child.gameObject.name.Contains("fracture")) {
				fractureList.Add(child.gameObject);
			}
		}

		// Add components to chunks
		rigidbodies=new Rigidbody[fractureList.Count];
		for (i=0; i<fractureList.Count; i++) {
			Mesh mesh= fractureList[i].GetComponent<MeshFilter>().sharedMesh;
			if (mesh.vertexCount>6) {
				MeshCollider collider = fractureList[i].AddComponent<MeshCollider>();
				Collider tCollider = ObjectReference.instance.terrainCollider;
				collider.sharedMesh=mesh;
				collider.convex=true;

				// Detect ground intersection (grounded pieces are stationary)
				Vector3 dir = Vector3.zero;
				float dist = 0f;
				if (Physics.ComputePenetration(collider,collider.transform.position,collider.transform.rotation,tCollider,tCollider.transform.position,tCollider.transform.rotation,out dir,out dist)==false) {
					rigidbodies[i]=fractureList[i].AddComponent<Rigidbody>();
					rigidbodies[i].mass=4f;
					rigidbodies[i].interpolation=RigidbodyInterpolation.Interpolate;
				}
				fractureList[i].layer=LayerMask.NameToLayer("Grabbable");
				fractureList[i].SetActive(false);
				fractureList[i].AddComponent<DebrisRigidbody>();
				SandPuff puffer=fractureList[i].AddComponent<SandPuff>();
				puffer.scaleMult=2f;
				puffer.playSounds=false;
			} else {
				Destroy(fractureList[i]);
			}
		}
		staticObject.AddComponent<DamageReporter>().prop=this;
		staticObject.layer=LayerMask.NameToLayer("Grabbable");
	}

	public void TakeDamage(float damage) {
		if (damage>minForceForDamage) {
			health-=damage;
			if (health<=0f) {
				Smash();
				enabled=false;
			}
		}
	}

	public void Smash() {
		int i;
		if (staticObject.activeSelf) {
			if (isStone) {
				Sounds.PlaySound("stone smash",transform.position);
			} else {
				Sounds.PlaySound("wood smash",transform.position);
			}
			staticObject.SetActive(false);
			for (i=0; i<rigidbodies.Length; i++) {
				if (fractureList[i]!=null) {
					if (rigidbodies[i]!=null) {
						rigidbodies[i].isKinematic=false;
					} else {
						fractureList[i].AddComponent<Rigidbody>().isKinematic=true;
					}
					fractureList[i].gameObject.SetActive(true);
				}
			}
		}
	}
}
