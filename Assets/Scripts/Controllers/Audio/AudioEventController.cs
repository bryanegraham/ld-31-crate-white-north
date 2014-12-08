using UnityEngine;
using System.Collections.Generic;

public class AudioEventController : MonoBehaviour
{
	public string EventName;
	public AudioClip[] Clips;
	public AudioSource[] Sources;

	public void Event(string event_)
	{
		if (event_ == EventName)
		{
			Play(RandomClip());
		}
	}

	void Play(AudioClip clip)
	{
		AudioSource src = GetSource();
		src.clip = clip;
		src.Play();
	}

	AudioSource GetSource()
	{
		foreach (AudioSource src in Sources)
		{
			if (!src.isPlaying)
			{
				return src;
			}
		}

		Debug.LogWarning("Actor " + gameObject.name + "ran out of audio sources");
		int i = Random.Range(0, Sources.Length);
		Sources[i].Stop();
		return Sources[i];
	}

	AudioClip RandomClip()
	{
		int i = Random.Range(0, Clips.Length);
		return Clips[i];
	}
}