using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] footstepSounds;
    private int footstepSoundsIndex;
    [SerializeField]
    private AudioClip[] hurtSounds;
    private int hurtSoundsIndex;
    [SerializeField]
    private AudioClip[] landSounds;
    private int landSoundsIndex;

    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip dashSound;

    private AudioSource soundSource;
    private float pitchLowRange = 0.95f;
    private float pitchHighRange = 1.05f;
    private float volumeLowRange = 0.85f;
    private float volumeHighRange = 1.15f;
    private float lastTime;
    private float overlapLock = 0.20f;

    private void Start()
    {
        soundSource = GetComponent<AudioSource>();
    }

    private void SwapAndPlaySound(AudioClip[] sound, int soundIndex)
    {
        soundSource.pitch = Random.Range(pitchLowRange, pitchHighRange);
        float volume = Random.Range(volumeLowRange, volumeHighRange);

        AudioClip clip = sound[soundIndex];
        soundSource.PlayOneShot(clip, volume);
        sound[soundIndex] = sound[0];
        sound[0] = clip;
    }

    private void PlayFootstepSound()
    {
        if(Time.time - lastTime >= overlapLock) //Förhindrar fotsteg att overlappa vid diagonal eller snabb rörelse
        {
            lastTime = Time.time;
            footstepSoundsIndex = Random.Range(1, footstepSounds.Length);
            SwapAndPlaySound(footstepSounds, footstepSoundsIndex);


            /*
            //Slumpar pitch och volym
            footstepSoundsIndex = Random.Range(0, footstepSounds.Length);
            soundSource.pitch = Random.Range(pitchLowRange, pitchHighRange);
            float volume = Random.Range(volumeLowRange, volumeHighRange);

            //Ignorerar första index, byter sedan plats på den och ljudet som spelades för att undvika upprepning
            AudioClip clip = footstepSounds[footstepSoundsIndex];
            soundSource.PlayOneShot(clip, volume);
            footstepSounds[footstepSoundsIndex] = footstepSounds[0];
            footstepSounds[0] = clip;
            */
        }
    }

    public void PlayHurtSound()
    {
        hurtSoundsIndex = Random.Range(1, hurtSounds.Length);
        SwapAndPlaySound(hurtSounds, hurtSoundsIndex);
        /*
        //Slumpar pitch och volym
        hurtSoundsIndex = Random.Range(0, hurtSounds.Length);
        soundSource.pitch = Random.Range(pitchLowRange, pitchHighRange);
        float volume = Random.Range(volumeLowRange, volumeHighRange);

        //Ignorerar första index, byter sedan plats på den och ljudet som spelades för att undvika upprepning
        AudioClip clip = hurtSounds[hurtSoundsIndex];
        soundSource.PlayOneShot(clip, volume);
        hurtSounds[hurtSoundsIndex] = hurtSounds[0];
        hurtSounds[0] = clip;
        */
    }

    private void PlayLandSound()
    {
        landSoundsIndex = Random.Range(1, landSounds.Length);
        SwapAndPlaySound(landSounds, landSoundsIndex);
        /*
        //Slumpar pitch och volym
        landSoundsIndex = Random.Range(0, landSounds.Length);
        soundSource.pitch = Random.Range(pitchLowRange, pitchHighRange);
        float volume = Random.Range(volumeLowRange, volumeHighRange);

        //Ignorerar första index, byter sedan plats på den och ljudet som spelades för att undvika upprepning
        AudioClip clip = landSounds[landSoundsIndex];
        soundSource.PlayOneShot(clip, volume);
        landSounds[landSoundsIndex] = landSounds[0];
        landSounds[0] = clip;
        */
    }

    public void PlayJumpSound()
    {
        //Slumpar pitch och volym
        soundSource.pitch = Random.Range(pitchLowRange, pitchHighRange);
        float volume = Random.Range(volumeLowRange, volumeHighRange);
        soundSource.PlayOneShot(jumpSound, volume);
    }

    public void PlayDashSound()
    {
        soundSource.PlayOneShot(dashSound, 1);
    }


}
