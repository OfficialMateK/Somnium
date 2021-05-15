using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaBlock : MonoBehaviour
{
    [SerializeField] private GameObject shield;
    [SerializeField] private GameObject shield2;

    // Start is called before the first frame update
    void Start()
    {
        shield.SetActive(false);
        shield2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider block)
    {
        if (block.CompareTag("Player"))
        {
            shield.SetActive(true);
            shield2.SetActive(true);
        }
    }

}
