using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour {
	public SoundEffect[] soundEffects;
	static Sounds instance;

	static Dictionary<string,SoundEffect> soundLookup;

	public static void PlaySound(string name,Vector3 position,float volumeMult=1f) {
		GameObject gameObj = new GameObject("Audio Source");
		gameObj.transform.position=position;
		SoundEffect sound = soundLookup[name];
		gameObj.transform.parent=instance.transform;
		AudioSource source=gameObj.AddComponent<AudioSource>();
		source.rolloffMode=AudioRolloffMode.Linear;
		source.maxDistance=sound.maxDistance;
		source.spatialBlend=1f;
		source.outputAudioMixerGroup=sound.mixerGroup;
		source.clip=sound.clips[Random.Range(0,sound.clips.Length)];
		source.pitch=Random.Range(sound.minPitch,sound.maxPitch);
		source.volume=Random.Range(sound.minVolume,sound.maxVolume)*volumeMult;

		source.Play();
		gameObj.AddComponent<DestroyOnSoundFinish>();
	}

	void Awake () {
		instance=this;

		soundLookup=new Dictionary<string,SoundEffect>();
		for (int i=0;i<soundEffects.Length;i++) {
			soundLookup.Add(soundEffects[i].name,soundEffects[i]);
		}
	}
}
