using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CapturePointUI : MonoBehaviour
{
    public Text chargeText;
    private float chargeInProcent;

    public void SetProgress(float charge)
    {
        chargeInProcent = (charge / 60) * 100;
        chargeText.text = ("Progress: " + chargeInProcent.ToString("f1") + "%");
    }
}
