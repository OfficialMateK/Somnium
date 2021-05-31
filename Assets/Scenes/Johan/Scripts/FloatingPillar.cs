using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingPillar : MonoBehaviour
{
    private float startDelay;
    private bool floatStarted = false;

    private float amplitude = 1f;
    private float frequency = 0.2f;

    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();


    void Start()
    {
        posOffset = transform.position;
        startDelay = Random.Range(1, 10);
        StartCoroutine(WaitForDelay());
    }

    void Update()
    {
        if(floatStarted)
        {
            //transform.position = new Vector3(transform.position.x, originalY + ((float)Mathf.Sin(Time.deltaTime) * floatStrength), transform.position.z);
            tempPos = posOffset;
            tempPos.y += Mathf.Sin((Time.fixedTime - startDelay) * Mathf.PI * frequency) * amplitude;

            transform.position = tempPos;
        }
    }

    IEnumerator WaitForDelay()
    {
        yield return new WaitForSeconds(startDelay);
        floatStarted = true;
    }
}
