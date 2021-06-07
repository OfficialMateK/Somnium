using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndCredits : MonoBehaviour
{
    

   
    public GameObject EndCreditUI;

    private void Start()
    {
        Cursor.visible = true;

        Cursor.lockState = CursorLockMode.Confined;
    }



    // Update is called once per frame
    void Update()
    {
        
    }

   

    public void Pause()
    {
        
        
        Cursor.visible = true;
        
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Debug.Log("Quiting Game...");
        Application.Quit();
    }

    
}
