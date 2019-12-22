using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System;

public class AudioManager : MonoBehaviour
{
	public Sound[] sounds;
	public static AudioManager Instance;
	public AudioMixerGroup audioMixer;
	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad (gameObject);
		}
	
		else
		{
			Destroy(gameObject);
			return;
		}
		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.outputAudioMixerGroup = audioMixer;
			s.source.volume = s.volume;
			s.source.pitch = s.pitch;
			s.source.loop = s.loop;
		}	
	}
	public void Start(){
		Play("Theme1");
	}
	public void Play(string name){
		Sound s = Array.Find(sounds, sound => sound.name == name);
		s.source.Play();
		if(s==null)
		{
			Debug.LogWarning("Sound: " + name+ " not found");
			return;
		}
	}
	
}