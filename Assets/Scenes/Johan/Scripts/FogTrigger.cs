using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogTrigger : MonoBehaviour
{
    [SerializeField] private ParticleSystem playerFogParticles;
    private bool isPlaying;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isPlaying)
        {
            playerFogParticles.Play();
            isPlaying = true;
        }
        else
        {
            playerFogParticles.Stop();
            isPlaying = false;
        }
    }
}
