using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class MainMenuManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartGame() {
        SceneManager.LoadScene("GameScene");
    }

    public void ViewRecords() {
        SceneManager.LoadScene("RecordsScene");
    }

    public void ExitGame() {
        Application.Quit();
    }
}
