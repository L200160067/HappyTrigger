using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class BGM
{
    public string name;
    public AudioClip clip;
    public AudioMixerGroup mixer;
    [HideInInspector]
    public AudioSource source;
}
