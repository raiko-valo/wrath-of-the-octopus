using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "AudioClipGroup")]
public class AudioClipGroup : ScriptableObject
{
    [Range(0, 2)]
    public float VolumeMin = 1.0f;
    [Range(0, 2)]
    public float VolumeMax = 1.0f;
    [Range(0, 2)]
    public float Pitchmin = 1.0f;
    [Range(0, 2)]
    public float Pitchmax = 1.0f;
    [Range(0, 2)]
    public float Cooldown = 0.1f;

    private float nextPlayTime = 0.0f;

    public List<AudioClip> AudioClips;

    public void Play(float pitchAdd = 0, Vector3? position = null)
    {
        Debug.Log("Play 1");
        Play(AudioSourcePool.instance.GetSource(), pitchAdd, position);
    }

    public void Play(AudioSource source, float pitchAdd = 0, Vector3? position = null)
    {
        Debug.Log("Play 2");
        Debug.Log(Time.time);
        Debug.Log(nextPlayTime);
        if (Time.time < nextPlayTime) return;
        nextPlayTime = Time.time + Cooldown;

        Debug.Log("Play 3");
        if (position != null)
        {
            source.spatialBlend = 1.0f;
            source.transform.position = position ?? Vector3.zero;
        }
        else source.spatialBlend = 0.0f;

        Debug.Log("Play 4");
        source.clip = AudioClips[Random.Range(0, AudioClips.Count)];
        source.volume = Random.Range(VolumeMin,VolumeMax);
        source.pitch = Random.Range(Pitchmin,Pitchmax) + pitchAdd;
        
        source.Play();
        Debug.Log("Play 5");

    }

    private void OnDisable()
    {
        nextPlayTime = 0;
    }
}
