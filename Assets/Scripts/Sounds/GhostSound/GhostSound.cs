using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSound : MonoBehaviour
{
    private AudioSource[] sources;
    public AudioClip ghost1;
    public AudioClip ghost2;
    public AudioClip ghost3;
    public AudioClip ghost4;
    public AudioClip ghost5;
    public float minDelay;
    public float maxDelay;


    // Start is called before the first frame update
    void Start()
    {
        sources = GetComponents<AudioSource>();
        sources[0].clip = ghost1;
        sources[1].clip = ghost2;
        sources[2].clip = ghost3;
        sources[3].clip = ghost4;
        sources[4].clip = ghost5;
    }

    // Update is called once per frame
    void Update()
    {
     if (!sources[0].isPlaying)
        {
            float d = Random.Range(minDelay, maxDelay);
            sources[0].PlayDelayed (d);
            Debug.Log("source[0] d = " + d );
        }
        if (!sources[1].isPlaying)
        {
            float d = Random.Range(minDelay, maxDelay);
            sources[1].PlayDelayed(d);
            Debug.Log("source[1] d = " + d);
        }
        if (!sources[2].isPlaying)
        {
            float d = Random.Range(minDelay, maxDelay);
            sources[2].PlayDelayed(d);
            Debug.Log("source[2] d = " + d);
        }
        if (!sources[3].isPlaying)
        {
            float d = Random.Range(minDelay, maxDelay);
            sources[3].PlayDelayed(d);
            Debug.Log("source[3] d = " + d);
        }
        if (!sources[4].isPlaying)
        {
            float d = Random.Range(minDelay, maxDelay);
            sources[4].PlayDelayed(d);
            Debug.Log("source[4] d = " + d);
        }

    }
}
