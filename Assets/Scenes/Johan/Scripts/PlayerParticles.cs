using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticles : MonoBehaviour
{

    [SerializeField] private ParticleSystem dashTrail;
    [SerializeField] private ParticleSystem leafParticles;


    public void PlayDashTrail(Vector3 movementDirection)
    {
        dashTrail.transform.rotation = Quaternion.LookRotation(-movementDirection);
        dashTrail.Play();
    }

    public void PlayLeafParticles(Vector3 movementDirection)
    {
        leafParticles.transform.rotation = Quaternion.LookRotation(-movementDirection);
        Instantiate(leafParticles, gameObject.transform.position, leafParticles.transform.rotation);
        //leafParticles.Play();
    }
}
