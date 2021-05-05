using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    public string levelName;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Changing Scene...");
        SceneManager.LoadScene(levelName);
    }
}
