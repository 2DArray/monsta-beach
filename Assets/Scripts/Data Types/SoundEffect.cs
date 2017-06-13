using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class SoundEffect {
	public string name;
	public AudioClip[] clips;
	public AudioMixerGroup mixerGroup;
	public float minVolume=.5f;
	public float maxVolume=1f;
	public float minPitch = .85f;
	public float maxPitch = 1.1f;
	public float maxDistance = 30f;
}
