using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageSoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] playerAudios;

    private AudioSource controlPlayerAudio;

    void Awake()
    {
        controlPlayerAudio = GetComponent<AudioSource>();
    }

    public void PlayerAudioSelection(int index, float volumen)
    {
        controlPlayerAudio.PlayOneShot(playerAudios[index], volumen);
    }

    public void StopSound()
    {
        if (controlPlayerAudio.isPlaying) controlPlayerAudio.Stop();
    }
}
