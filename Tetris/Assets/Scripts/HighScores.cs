using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HighScores : MonoBehaviour {

    public Text recordText;
    public Text nameText;
    public Text scoresText;
    private string recordsResult;
    private string namesResult;
    private string scoresResult;

	// Use this for initialization
	void Start () {
        recordsResult = "№\n";
        namesResult = "Name\n";
        scoresResult = "Scores\n";
        for (int i = 0; i < 10; i++) {
            // Make keys
            string currentScoreRecord = "Score" + (i+1).ToString();
            string currentNameRecord = "Name" + (i + 1).ToString();
            // Get values
            int scores = PlayerPrefs.GetInt(currentScoreRecord, 0);
            string name = PlayerPrefs.GetString(currentNameRecord, "No name");
            // Modifiying resul string
            recordsResult += string.Format("{0}.\n", (i+1).ToString());
            namesResult += string.Format("{0}.\n", name);
            scoresResult += string.Format("{0}.\n", scores);
        }
        // Set text
        recordText.text = recordsResult;
        nameText.text = namesResult;
        scoresText.text = scoresResult;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
