using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSystem : MonoBehaviour {
    
    public void PlayAgain()
    {
        Application.LoadLevel("SleepScene");
    }

    public void Settings()
    {
        Application.LoadLevel("SettingsScene");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        Application.LoadLevel("MainMenuScene");
    }
}
