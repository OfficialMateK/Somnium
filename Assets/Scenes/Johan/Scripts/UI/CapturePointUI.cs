using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CapturePointUI : MonoBehaviour
{
    private TMP_Text progressText;
    private float chargeInProcent;

    private void Start()
    {
        progressText = GetComponent<TMP_Text>();
    }

    public void SetProgress(float charge)
    {
        chargeInProcent = (charge / 60) * 100;
        progressText.text = ("Progress: " + chargeInProcent.ToString("f1") + "%");
        
    }
}
