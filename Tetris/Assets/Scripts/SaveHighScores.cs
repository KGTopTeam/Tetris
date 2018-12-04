using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SaveHighScores : MonoBehaviour {

    public InputField inputNameField;

	// Use this for initialization
	void Start () {
        //MakeEmptyScoreRecords();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GetInputName(string playerName) {
        int playerScores = PlayerPrefs.GetInt("CurrentScore");
        
        // Debug of values
        
        if (string.IsNullOrEmpty(playerName)) {
            Debug.Log("EMPTY");
        } else {
            SavePlayerScores(playerName, playerScores);
            Destroy(inputNameField.gameObject);
            Debug.Log("Player name is " + playerName);
            Debug.Log(playerName + " score is " + playerScores);
        }
    }

    private void SavePlayerScores(string name, int scores) {
        if (scores <= 0) {
            return;
        // If there is no records set first score
        } else if (!PlayerPrefs.HasKey("Score1")) {
            PlayerPrefs.SetInt("Score1", scores);
            PlayerPrefs.SetString("Name1", name);
        } else {
            for(int i = 0; i < 10; i++) {
                string currentScoreRecord = "Score" + (i+1).ToString();
                string currentNameRecord = "Name" + (i + 1).ToString();

                if (PlayerPrefs.HasKey(currentScoreRecord)) {
                    Debug.Log(currentNameRecord + ": " + PlayerPrefs.GetString(currentNameRecord) + " " +
                        currentScoreRecord + ": " + PlayerPrefs.GetInt(currentScoreRecord));
                    if (PlayerPrefs.GetInt(currentScoreRecord) < scores) {
                        Debug.Log("START SHIFT");
                        ShiftHighScores(i, scores, name);
                        break;
                    }
                } else {
                    PlayerPrefs.SetInt(currentScoreRecord, scores);
                    PlayerPrefs.SetString(currentNameRecord, name);
                    break;
                }
            }
        }
    }

    private void ShiftHighScores(int position, int score, string name) {
        // Shit here
        int currentScore = score;
        string currentName = name;

        for(int i = position; i < 10; i++) {
            string currentScoreRecord = "Score" + (i + 1).ToString();
            string currentNameRecord = "Name" + (i + 1).ToString();

            // While there is key just shift values
            if (PlayerPrefs.HasKey(currentScoreRecord)) {
                // Save previous scores and name
                int previousScore = PlayerPrefs.GetInt(currentScoreRecord);
                string previousName = PlayerPrefs.GetString(currentNameRecord);

                // Set new value
                PlayerPrefs.SetInt(currentScoreRecord, currentScore);
                PlayerPrefs.SetString(currentNameRecord, currentName);

                // And now previous scores and name become current for next interation
                currentScore = previousScore;
                currentName = previousName;
            // If it was last key then make new record and break
            } else {
                PlayerPrefs.SetInt(currentScoreRecord, currentScore);
                PlayerPrefs.SetString(currentNameRecord, currentName);
                break;
            }
        }
    }

    private void MakeEmptyScoreRecords() {
        for (int i = 0; i < 10; i++)
        {
            string currentScoreRecord = "Score" + (i + 1).ToString();
            string currentNameRecord = "Name" + (i + 1).ToString();
            PlayerPrefs.SetInt(currentScoreRecord, 0);
            PlayerPrefs.SetString(currentNameRecord, "No name");
        }
    }
}
