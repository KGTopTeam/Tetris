﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameplayManager : MonoBehaviour {
    public Text timerText;

	// Use this for initialization
	void Start () {
        StartCoroutine(CountToStart(4, "Start"));
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public void ResumeGame() {
        StartCoroutine(CountToStart(4, "Continue"));
    }

    private IEnumerator CountToStart(int seconds, string textMessage)
    {
        while (seconds > 0)
        {
            seconds--;
            if(seconds == 0) {
                timerText.text = textMessage;
            }
            else {
                timerText.text = seconds.ToString();
            }
            yield return new WaitForSeconds(1);
        }
        timerText.text = null;
    }
}