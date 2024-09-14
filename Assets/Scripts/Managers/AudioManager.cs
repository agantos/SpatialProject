using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip typingSound;
	public AudioSource typingAudioSource;

    public static AudioManager Instance { get; private set; }

	private void Start()
	{

		if(Instance == null)
		{
			Instance = this;
		}
	}

	public void PlayTypingSoundEffect()
	{
		typingAudioSource.clip = typingSound;
		typingAudioSource.Play();
	}

	public void StopTypingSoundEffect()
	{
		typingAudioSource.Stop();
	}
}
