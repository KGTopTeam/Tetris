using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuSystem : MonoBehaviour {
    
    public void PlayAgain()
    {
        SceneManager.LoadScene("SleepScene");
    }

    public void Settings()
    {
        SceneManager.LoadScene("SettingsScene");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void HighScores()
    {
        SceneManager.LoadScene("HighScores");
    }
}
