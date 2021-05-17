using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIAppear : MonoBehaviour
{

    [SerializeField] private Image customImage;
    [SerializeField] private TMP_Text customText;



    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            customImage.enabled = true;
            customText.enabled = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            customImage.enabled = false;
            customText.enabled = false;
        }
    }

}