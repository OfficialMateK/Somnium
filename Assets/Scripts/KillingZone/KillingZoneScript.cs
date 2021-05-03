using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class KillingZoneScript : MonoBehaviour
{
    public GameObject quest;
    public GameObject progress;
    private TextMeshPro questText;
    private TextMeshPro progressText;

    private void Start()
    {
        questText = quest.GetComponent<TextMeshPro>();
        progressText = progress.GetComponent<TextMeshPro>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            progress.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            progress.SetActive(false);
        }
    }
}
