using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPillar : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            animator.SetTrigger("PlayerEntered");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetTrigger("PlayerExited");
        }
    }
}
