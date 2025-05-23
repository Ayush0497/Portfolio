using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHub : MonoBehaviour
{
    AudioSource[] source;

    private void Awake()
    {
        source = GetComponents<AudioSource>();
    }

    public void PlayCoinSound()
    {
        source[0].Play();
    }

    public void PlayJumpSound()
    {
        source[1].Play();
    }
    public void PlayOuchSound()
    {
        source[2].Play();
    }
}
