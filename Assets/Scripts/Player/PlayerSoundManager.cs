using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] playerAudios;

    private AudioSource controlPlayerAudio;

    void Start()
    {
        controlPlayerAudio = GetComponent<AudioSource>();
    }

    public void PlayerAudioSelection(int index, float volumen)
    {
        if (index != -1)
        {
            controlPlayerAudio.pitch = 1;
            controlPlayerAudio.PlayOneShot(playerAudios[index], volumen);
        }
        else
        {
            index = 0;
            controlPlayerAudio.pitch = 2;
            controlPlayerAudio.PlayOneShot(playerAudios[index], volumen);
        }
    }

    public void StopSound()
    {
        if (controlPlayerAudio.isPlaying) controlPlayerAudio.Stop();
    }
}
