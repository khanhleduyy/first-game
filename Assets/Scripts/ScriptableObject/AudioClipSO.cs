using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/AudioClipSO")]
public class AudioClipSO : ScriptableObject
{
    public AudioClip shoot;
    public AudioClip jump;
    public AudioClip cardChoose;
    public AudioClip explode;
}
