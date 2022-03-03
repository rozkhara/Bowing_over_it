using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneButtons : MonoBehaviour
{

    public void ExitButton()
    {
        Application.Quit();
    }

    public void NewGameButton()
    {
        if (Time.timeScale == 0f) Time.timeScale = 1f;
        string tmp = SceneManager.GetActiveScene().name;
        if (tmp != "MainScene" && tmp != "Tutorial")
        {
            PlayerPrefs.SetString("LastPlayedLevel", tmp);
            PlayerPrefs.Save();
        }
        SceneManager.LoadScene("LevelSelectionScene");
    }

    public void HowToPlayButton()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void MainButton()
    {
        if (Time.timeScale == 0f) Time.timeScale = 1f;
        SceneManager.LoadScene("MainScene");
    }
}
