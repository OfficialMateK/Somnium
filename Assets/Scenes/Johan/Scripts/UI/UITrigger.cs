using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITrigger : MonoBehaviour
{
    private UIChangeTrigger changeTrigger;

    // Start is called before the first frame update
    void Start()
    {
        changeTrigger = GetComponentInParent<UIChangeTrigger>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            changeTrigger.triggerUIChange(0);
        }
    }
}
