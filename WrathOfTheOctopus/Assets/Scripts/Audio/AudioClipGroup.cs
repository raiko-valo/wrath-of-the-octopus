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
        Play(AudioSourcePool.instance.GetSource(), pitchAdd, position);
    }

    public void Play(AudioSource source, float pitchAdd = 0, Vector3? position = null)
    {
        if (Time.time < nextPlayTime) return;
        nextPlayTime = Time.time + Cooldown;

        if (position != null)
        {
            source.spatialBlend = 1.0f;
            source.transform.position = position ?? Vector3.zero;
        }
        else source.spatialBlend = 0.0f;

        source.clip = AudioClips[Random.Range(0, AudioClips.Count)];
        source.volume = Random.Range(VolumeMin,VolumeMax);
        source.pitch = Random.Range(Pitchmin,Pitchmax) + pitchAdd;
        
        source.Play();
    }

    private void OnDisable()
    {
        nextPlayTime = 0;
    }
}
