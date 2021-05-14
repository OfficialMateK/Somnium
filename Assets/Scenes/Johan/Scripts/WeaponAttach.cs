using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttach : MonoBehaviour
{
    public Transform hand;

    // Start is called before the first frame update
    void Start()
    {
        transform.SetParent(hand);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
