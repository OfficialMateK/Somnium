using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CrystalUI : MonoBehaviour
{
    private TMP_Text healthText;

    private void Start()
    {
        healthText = GetComponent<TMP_Text>();   
    }

    public void SetHealth(float health)
    {
        healthText.text = ("Crystal Health: " + health.ToString("f0")) ;
    }
}
