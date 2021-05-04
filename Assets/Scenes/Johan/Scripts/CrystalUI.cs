using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrystalUI : MonoBehaviour
{
    public Text healthText;


    public void SetHealth(float health)
    {
        healthText.text = ("Crystal Health: " + health.ToString("f0")) ;
    }
}
