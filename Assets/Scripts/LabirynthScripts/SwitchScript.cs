using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScript : MonoBehaviour
{
    public GameObject door;

    private bool isClosed;
    private GameObject colliderObject;
    void Start()
    {
        isClosed = true;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(colliderObject != null)
            {
                switch(isClosed)
                {
                    case true:
                        door.SetActive(false);
                        isClosed = false;
                        break;
                    case false:
                        door.SetActive(true);
                        isClosed = true;
                        break;
                    default:
                        break;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            colliderObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            colliderObject = null;
        }
    }
}
