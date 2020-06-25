using UnityEngine;
using System;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
	public Sound[] sounds;

	public static AudioManager instance;

	void Awake()
	{
		if (instance == null) { instance = this; }
		else { Destroy(gameObject); }

		DontDestroyOnLoad(gameObject);

		foreach (Sound s in sounds)
		{
			// Create an audio source for each sound in sounds array
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.volume = s.volume;
			s.source.pitch = s.pitch;
			s.source.loop = s.loop;
		}
	}

	void Start()
	{
		Play("BGM");
	}

	public void Play(string name)
	{
		Sound s = Array.Find(this.sounds, sound => sound.name == name);

		// If we found the sound, play it
		if (s == null) { Debug.LogWarning("Sound: " + name + " not found!"); }
		else { s.source.Play(); }
	}
}
