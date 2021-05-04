using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIChangeTrigger : MonoBehaviour
{
    public GameObject UIToChangeTo;
    public GameObject defaultUI;



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            defaultUI.SetActive(false);
            UIToChangeTo.SetActive(true);
        }
    }
}
