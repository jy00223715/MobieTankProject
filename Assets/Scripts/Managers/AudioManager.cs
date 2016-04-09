using UnityEngine;
using System.Collections;

public class AudioManager
{
    public static void AudioPlayOneShot(AudioSource source, AudioClip clip)
    {
        source.PlayOneShot(clip);
    }

    public static void AudioPlay(AudioSource source, AudioClip clip)
    {
        source.clip = clip;
        source.Play();
    }

    public static void AudioPlayAtPos(AudioClip clip, Vector3 pos)
    {
        AudioSource.PlayClipAtPoint(clip, pos);
    }
}