using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject controls;




    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }

        }
    }

    public void Resume()
    {
        controls.SetActive(false);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        Cursor.visible = false;
        GameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        Cursor.visible = true;
        GameIsPaused = true;
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

    public void SaveGame()
    {
        //GameObject player = GameObject.Find("Player");
       // GameObject gameManager = GameObject.Find("GameManager");

        //Player Data
        /*
        SaveData.current.playerSaveData.health = player.GetComponent<PlayerHealth>().GetHealth();
        //SaveData.current.playerSaveData.ammoCurrent =
        //SaveData.current.playerSaveData.ammoBackup =
        SaveData.current.playerSaveData.position[0] = player.transform.position.x;
        SaveData.current.playerSaveData.position[1] = player.transform.position.y;
        SaveData.current.playerSaveData.position[2] = player.transform.position.z;

        //Level Data
        SaveData.current.levelNumber = gameManager.GetComponent<GameManager>().GetSceneNumber();
        SaveData.current.crystalCompleted = gameManager.GetComponent<GameManager>().GetCrystalCompleted();
        SaveData.current.collectionCompleted = gameManager.GetComponent<GameManager>().GetCollectionCompleted();
        SaveData.current.captureCompleted = gameManager.GetComponent<GameManager>().GetCaptureCompleted();
        */

        GameObject player = GameObject.Find("Player");
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        PlayerSaveSystem.SavePlayer(player,gameManager);
    }


}
