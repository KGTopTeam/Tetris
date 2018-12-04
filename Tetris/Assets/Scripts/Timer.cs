using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public Text timerText;

    private bool readyToPlay = false;

    // Use this for initialization
    void Start () {
        StartCoroutine(CountToStart(4, "Start"));
    }

    void Update()
    {
        if (readyToPlay)
        {
            StartMainScene();
        }
    }

    private IEnumerator CountToStart(int seconds, string textMessage)
    {
        while (seconds > 0)
        {
            seconds--;
            if (seconds == 0)
            {
                timerText.text = textMessage;

                readyToPlay = true;
            }
            else
            {
                timerText.text = seconds.ToString();
            }
            yield return new WaitForSeconds(1);
        }
        timerText.text = null;
    }

    private void StartMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}
