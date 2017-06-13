using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowEventCatcher : MonoBehaviour {
	Defender defender;
	void Start() {
		defender=GetComponentInParent<Defender>();
	}

	void ThrowEvent() {
		defender.ThrowEvent();
	}
}
