using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] audios;

    private AudioSource controlAudio;

    void Awake()
    {
        controlAudio = GetComponent<AudioSource>();
    }

    public void AudioSelection(int index, float volumen)
    {
        controlAudio.PlayOneShot(audios[index], volumen);
    }
}
