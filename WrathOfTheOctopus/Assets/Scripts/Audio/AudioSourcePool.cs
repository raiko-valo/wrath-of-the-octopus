using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioSourcePool : MonoBehaviour
{
    public AudioSource audioSourcePrefab;
    public static AudioSourcePool instance;
    private List<AudioSource> audioSources = new List<AudioSource>();

    private void Awake()
    {
        instance = this;
    }

    public AudioSource GetSource()
    {
        foreach (AudioSource source in audioSources)
            if (!source.isPlaying) return source;
        AudioSource newAudio = Instantiate(audioSourcePrefab,transform);
        audioSources.Add(newAudio);
        return newAudio;
    }
}
