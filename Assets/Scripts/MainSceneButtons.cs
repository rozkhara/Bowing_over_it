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
        SceneManager.LoadScene("LevelSelectionScene");
    }
    
    public void HowToPlayButton()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void MainButton()
    {
        SceneManager.LoadScene("MainScene");
    }
}
