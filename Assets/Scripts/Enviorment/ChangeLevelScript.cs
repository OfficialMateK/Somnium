using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevelScript : MonoBehaviour
{
    public string nextLevel;

    private bool pressed;
    private void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            pressed = true;
        } else
        {
            pressed = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(pressed)
        {
            Debug.Log("Changing Level...");
            SceneManager.LoadScene(nextLevel);
        }
    }
}
