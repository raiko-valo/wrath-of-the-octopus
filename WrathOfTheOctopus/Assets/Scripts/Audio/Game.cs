using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Game : MonoBehaviour
{
    public List<AudioClipGroup> audioClipGroups;
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            audioClipGroups[0].Play();
        }else if (Input.GetKey(KeyCode.Alpha2))
        {
            audioClipGroups[1].Play();
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            audioClipGroups[2].Play();
        }
    }
}
