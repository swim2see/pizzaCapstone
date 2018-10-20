using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundScript : MonoBehaviour {
	public static soundScript me;
	public AudioSource[] sources;
	public GameObject sourcePrefab;
	public int sourceNum;
	//to call this function: sound.me.play();
	public void Awake() {
		if (me != null) {
			Destroy(this);
		}
		me = this;

		sources = new AudioSource[sourceNum];
		for (int i = 0; i < sourceNum; i++) {
			sources[i] = ((GameObject)Instantiate(sourcePrefab, Vector3.zero, Quaternion.identity, transform)).GetComponent<AudioSource>();
		}
	}

	public void Play(AudioClip clip, float volume, float pitch, float pan, Vector2 pos) {
		int i = GetSourceInd();
		sources[i].clip = clip;
		sources[i].volume = volume;
		sources[i].pitch = pitch;
		if (pos != Vector2.zero) {
			sources[i].spatialBlend = 1;
			sources[i].transform.position = pos;
		} else {
			sources[i].spatialBlend = 0;
			sources[i].panStereo = pan;
		}
		sources[i].Play();
	}

	public void Play(AudioClip clip) {
		Play(clip, 1, 1, 0, Vector2.zero);
	}
	public void Play(AudioClip clip, float volume) {
		Play(clip, volume, 1, 0, Vector2.zero);

	}
	public void Play(AudioClip clip, float volume, float pitch) {
		Play(clip, volume, pitch, 0, Vector2.zero);

	}
	public void Play(AudioClip clip, float volume, float pitch, float pan) {
		Play(clip, volume, pitch, pan, Vector2.zero);

	}
	public void Play(AudioClip clip, float volume, float pitch, Vector2 pos) {
		Play(clip, volume, pitch, 0, pos);

	}
	public void Play(AudioClip clip,  Vector2 pos) {
		Play(clip, 1, 0, 0, pos);

	}
	public void Play(AudioClip clip, float volume, Vector2 pos) {
		Play(clip, volume, 0, 0, pos);

	}


	public int GetSourceInd() {
		for (int i = 0; i < sourceNum; i++) {
			if (!sources[i].isPlaying) {
				return i;
			}
		}
		Debug.Log("RAN OUT OF SOURCES DUDE");
		return 0;

	}
}