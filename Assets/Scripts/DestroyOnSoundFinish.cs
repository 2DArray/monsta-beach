using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnSoundFinish : MonoBehaviour {
	AudioSource source;

	int delayTicker = 0;
	void Start () {
		source=GetComponent<AudioSource>();
	}
	
	void Update () {
		if (delayTicker>10) {
			if (source.isPlaying==false) {
				//Debug.Log("WOOO");
				Destroy(gameObject);
			}
		}
		delayTicker++;
	}
}
