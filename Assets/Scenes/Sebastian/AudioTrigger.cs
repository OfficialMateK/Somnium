using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class AudioTrigger : MonoBehaviour
{

    private AudioSource[] sources;
    public AudioClip clip;
    public AudioClip fx;

    public AudioMixerSnapshot doldrum;
    public AudioMixerSnapshot loner;
    public AudioMixerSnapshot debate;

    private float lowVolRange = 10.0f;
    private float highVolRange = 25.0f;
    private float lowPitchRange = 10.0f;
    private float highPitchRange = 25.0f;


    // Start is called before the first frame update
    void Start()
    {
        sources = GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("InnerVoice"))
        {
            sources[0].PlayOneShot(clip);
        }

        if (col.gameObject.CompareTag("Doldrum"))
        {
            doldrum.TransitionTo(2.0f);
        }

        if (col.gameObject.CompareTag("Loner"))
        {
            loner.TransitionTo(2.0f);
        }

        if (col.gameObject.CompareTag("Debate"))
        {
            debate.TransitionTo(2.0f);
        }

        if (col.gameObject.CompareTag("Machine"))
        {
            sources[0].pitch = Random.Range(lowPitchRange, highPitchRange);
            float vol = Random.Range(lowVolRange, highVolRange);
            sources[0].PlayOneShot(fx);
        }

    }

}

