using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour {

    public static int gridWight = 10;
    public static int gridHeight = 20;

    public ScoreConfig scoreConfig;

    public FormsConfig formsConfig;

    private bool allowT = true;
    private bool allowS = true;
    private bool allowZ = true;
    private bool allowSquare = true;
    private bool allowLong = true;
    private bool allowL = true;
    private bool allowJ = true;

    public float fallSpeed = 1.0f;
    private int scoreOneLine = 5;
    private int scoreTwoLine = 15;
    private int scoreThreeLine = 45;
    private int scoreFourLine = 150;

    private AudioSource audioSource;

    private GameObject nextTetrisminoObj;

    private string nextTetrismino;

    private string currentTetrismino;

    public AudioClip lineCrearedAudio;

    public Text pauseText;

    public Text pub_score;

    public int currentScore;

    private int numberOfLinesThisTurn = 0;

    private int scoreL = 0;
    private int scoreJ = 0;
    private int scoreSquare = 0;
    private int scoreS = 0;
    private int scoreZ = 0;
    private int scoreLong = 0;
    private int scoreT = 0;

    public Text scoreTextL;
    public Text scoreTextJ;
    public Text scoreTextSquare;
    public Text scoreTextS;
    public Text scoreTextZ;
    public Text scoreTextLong;
    public Text scoreTextT;

    public Text scoreLines;

    public Text scoreLevel;

    public int currentLevel = 0;
    private int numLinesCleared = 0;
 
    public static Transform[,] grid = new Transform[gridWight, gridHeight];

	// Use this for initialization
	void Start () {
        LoadSettingsForms();

        LoadSettingsScore();

        currentTetrismino = GetRandomTetrismino();

        nextTetrismino = GetRandomTetrismino();

        SpawnNextTetrismino();

        audioSource = GetComponent<AudioSource>();

        Debug.Log(scoreOneLine + " " + scoreTwoLine + " " + scoreThreeLine + " " + scoreFourLine);

        
        if (allowJ)
        {
            Debug.Log("Allow J");
        }
        if (allowL)
        {
            Debug.Log("Allow L");
        }
        if (allowLong)
        {
            Debug.Log("Allow Long");
        }
        if (allowS)
        {
            Debug.Log("Allow S");
        }
        if (allowZ)
        {
            Debug.Log("Allow Z");
        }
        if (allowSquare)
        {
            Debug.Log("Allow Square");
        }
        if (allowT)
        {
            Debug.Log("Allow T");
        }
	}

    void Update ()
    {
        UpdateScore();
        UpdateUI();
        CheckUserInput();

        UpdateLevel();

        UpdateSpeed();
    }

    void UpdateLevel()
    {
        currentLevel = numLinesCleared / 10;
    }

    void UpdateSpeed()
    {
        fallSpeed = 1.0f - ((float)currentLevel * 0.1f);
    }

    public void UpdateUI()
    {
        pub_score.text = currentScore.ToString();
        if (allowJ)
        {
            scoreTextJ.text = scoreJ.ToString();
        }
        else
        {
            scoreTextJ.text = "---";
        }
        if (allowL)
        {
            scoreTextL.text = scoreL.ToString();
        }
        else
        {
            scoreTextL.text = "---";
        }
        if (allowS)
        {
            scoreTextS.text = scoreS.ToString();
        }
        else
        {
            scoreTextS.text = "---";
        }
        if (allowZ)
        {
            scoreTextZ.text = scoreZ.ToString();
        }
        else
        {
            scoreTextZ.text = "---";
        }
        if (allowSquare)
        {
            scoreTextSquare.text = scoreSquare.ToString();
        }
        else
        {
            scoreTextSquare.text = "---";
        }
        if (allowT)
        {
            scoreTextT.text = scoreT.ToString();
        }
        else
        {
            scoreTextT.text = "---";
        }
        if (allowLong)
        {
            scoreTextLong.text = scoreLong.ToString();
        }
        else
        {
            scoreTextLong.text = "---";
        }

        scoreLevel.text = currentLevel.ToString();

        scoreLines.text = numLinesCleared.ToString();
    }

    public void UpdateScore(){
        if(numberOfLinesThisTurn > 0)
        {
            if(numberOfLinesThisTurn == 1)
            {
                ClearedOneLine();
                numLinesCleared++;
            }
            else if(numberOfLinesThisTurn == 2)
            {
                ClearedTwoLine();
                numLinesCleared += 2;
            }
            else if(numberOfLinesThisTurn == 3)
            {
                ClearedThreeLine();
                numLinesCleared += 3;
            }
            else if(numberOfLinesThisTurn == 4)
            {
                ClearedFourLine();
                numLinesCleared += 4;
            }

            PlayLineClearedAudio();

            numberOfLinesThisTurn = 0;
        }
    }

    public void ClearedOneLine()
    {
        currentScore += scoreOneLine;
    }

    public void ClearedTwoLine()
    {
        currentScore += scoreTwoLine;
    }

    public void ClearedThreeLine()
    {
        currentScore += scoreThreeLine;
    }

    public void ClearedFourLine()
    {
        currentScore += scoreFourLine;
    }

    public bool CheckIsAboveGrid(Tetrismino tetrismino)
    {
        for (int x = 0; x < gridWight; ++x)
        {
            foreach (Transform mino in tetrismino.transform) {
                Vector2 pos = Round(mino.position);

                if (pos.y > gridHeight - 1)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public bool IsFullRowAt (int y)
    {
        for (int x = 0; x < gridWight; ++x)
        {
            if (grid[x,y] == null)
            {
                return false;
            }
        }

        numberOfLinesThisTurn++;

        return true;
    }

    public void DeleteMinoAt(int y)
    {
        for (int x = 0; x < gridWight; ++x)
        {
            Destroy(grid[x, y].gameObject);

            grid[x, y] = null; 
        }
    }

    public void MoveRowDown(int y)
    {
        for (int x=0; x < gridWight; ++x)
        {
            if (grid[x,y] != null)
            {
                grid[x, y - 1] = grid[x, y];

                grid[x, y] = null;

                grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    public void MoveAllRownDown(int y)
    {
        for (int i = y; i < gridHeight; ++i)
        {
            MoveRowDown(i);
        }
    }

    public void DeleteRow()
    {
        for(int y = 0; y < gridHeight; ++y)
        {
            if (IsFullRowAt(y))
            {
                DeleteMinoAt(y);

                MoveAllRownDown(y + 1);

                --y;
            }
        }
    }

    public void UpdateGrid (Tetrismino tetrismino)
    {
        for (int y = 0; y < gridHeight; ++y){
            for (int x = 0; x < gridWight; ++x)
            {
                if (grid[x,y] != null)
                {
                    if (grid[x,y].parent == tetrismino.transform)
                    {
                        grid[x, y] = null;
                    }
                }
            }
        }

        foreach (Transform mino in tetrismino.transform)
        {
            Vector2 pos = Round(mino.position);

            if (pos.y < gridHeight)
            {
                grid[(int)pos.x, (int)pos.y] = mino;
            }
        }
    }

    public Transform GetTransformAtGridPosition (Vector2 pos)
    {
        if (pos.y > gridHeight - 1)
        {
            return null;
        }
        else
        {
            return grid[(int)pos.x, (int)pos.y];
        }
    }

    public void SpawnNextTetrismino()
    {
        GameObject nextTetromino = (GameObject)Instantiate(Resources.Load(currentTetrismino, typeof(GameObject)), new Vector2(5.0f, 20.0f), Quaternion.identity);

        if(currentTetrismino == "Prefabs/Tetrismino_t")
        {
            scoreT++;
        } else if (currentTetrismino == "Prefabs/Tetrismino_j")
        {
            scoreJ++;
        } else if (currentTetrismino == "Prefabs/Tetrismino_s")
        {
            scoreS++;
        } else if (currentTetrismino == "Prefabs/Tetrismino_z")
        {
            scoreZ++;
        } else if (currentTetrismino == "Prefabs/Tetrismino_long")
        {
            scoreLong++;
        } else if (currentTetrismino == "Prefabs/Tetrismino_square")
        {
            scoreSquare++;
        } else if (currentTetrismino == "Prefabs/Tetrismino_l")
        {
            scoreL++;
        }

        currentTetrismino = nextTetrismino;

        if (nextTetrisminoObj != null)
        {
            Destroy(nextTetrisminoObj);
        }


        nextTetrisminoObj = (GameObject)Instantiate(Resources.Load(nextTetrismino + "_next", typeof(GameObject)), new Vector2(20.0f, 14.0f), Quaternion.identity);
        
        nextTetrismino = GetRandomTetrismino();
    }

    public bool CheckInsideGrid (Vector2 pos)
    {
        return ((int)pos.x >= 0 && (int)pos.x < gridWight && (int)pos.y >= 0);
    }

    public Vector2 Round (Vector2 pos)
    {
        return new Vector2(Mathf.Round(pos.x), Mathf.Round(pos.y));
    }

    string GetRandomTetrismino ()
    {
        int randomTetrismino = Random.Range(1, 8);
        string randomTetrisminoName = "Prefabs/Tetrismino_t";

        switch (randomTetrismino)
        {
            case 1:
                if (allowT)
                {
                    randomTetrisminoName = "Prefabs/Tetrismino_t";
                }
                else
                {
                    randomTetrisminoName = GetRandomTetrismino();
                }
                break;
            case 2:
                if (allowLong)
                {
                    randomTetrisminoName = "Prefabs/Tetrismino_long";
                }
                else
                {
                    randomTetrisminoName = GetRandomTetrismino();
                }
                break;
            case 3:
                if (allowSquare)
                {
                    randomTetrisminoName = "Prefabs/Tetrismino_square";
                }
                else
                {
                    randomTetrisminoName = GetRandomTetrismino();
                }
                break;
            case 4:
                if (allowJ)
                {
                    randomTetrisminoName = "Prefabs/Tetrismino_j";
                }
                else
                {
                    randomTetrisminoName = GetRandomTetrismino();
                }
                break;
            case 5:
                if (allowL)
                {
                    randomTetrisminoName = "Prefabs/Tetrismino_l";
                }
                else
                {
                    randomTetrisminoName = GetRandomTetrismino();
                }
                break;
            case 6:
                if (allowS)
                {
                    randomTetrisminoName = "Prefabs/Tetrismino_s";
                }
                else
                {
                    randomTetrisminoName = GetRandomTetrismino();
                }
                break;
            case 7:
                if (allowZ)
                {
                    randomTetrisminoName = "Prefabs/Tetrismino_z";
                }
                else
                {
                    randomTetrisminoName = GetRandomTetrismino();
                }
                break;
        }
        return randomTetrisminoName;
    }

    public void GameOver ()
    {
       PlayerPrefs.SetInt("CurrentScore", currentScore);
       SceneManager.LoadScene("GameOver");
    }

    public void PlayLineClearedAudio()
    {
        audioSource.PlayOneShot(lineCrearedAudio);
    }

    public void PrintPauseString(string text)
    {
        pauseText.text = text;
    }

    public void CheckUserInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenuScene");
        }
    }

    public void LoadSettingsScore()
    {
        if (scoreConfig != null)
        {
            scoreOneLine = scoreConfig.pointForOneLines;
            scoreTwoLine = scoreConfig.pointForTwoLines;
            scoreThreeLine = scoreConfig.pointForThreeLines;
            scoreFourLine = scoreConfig.pointForFourLines;
        }
    }

    public void LoadSettingsForms()
    {
        if (formsConfig != null)
        {
            allowJ = formsConfig.allowJ;
            allowL = formsConfig.allowL;
            allowLong = formsConfig.allowLong;
            allowS = formsConfig.allowS;
            allowT = formsConfig.allowT;
            allowZ = formsConfig.allowZ;
            allowSquare = formsConfig.allowSquare;
        }
    }
}
