using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
  public void PlayGame()
    {
        PlayerPrefs.SetInt("PlayerHealth", 200);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame ()
    {
        Application.Quit();
    }

    public void LoadGame()
    {
        PlayerSaveData data = PlayerSaveSystem.LoadPlayer();
        //SaveData.current = (SaveData)SerializationManager.Load(Application.persistentDataPath + "/saves/Save.save");

    }

}
