using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockFirstBridge : MonoBehaviour
{
    [SerializeField] private GameObject barrier;
    
    // Start is called before the first frame update
    void Start()
    {
        barrier.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider block)
    {
        if(block.CompareTag("Player"))
        {
            barrier.SetActive(true);
            
        }
        
    }
}
