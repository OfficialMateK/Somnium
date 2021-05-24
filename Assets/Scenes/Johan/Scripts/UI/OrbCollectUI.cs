using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OrbCollectUI : MonoBehaviour
{
    private TMP_Text remainingTextOrb;

    // Start is called before the first frame update
    void Start()
    {
        remainingTextOrb = GetComponent<TMP_Text>();
    }

    public void PickupOrb(int orbsCollected)
    {
        remainingTextOrb.text = "Orbs remaining: " + (3 - orbsCollected);
    }
}
