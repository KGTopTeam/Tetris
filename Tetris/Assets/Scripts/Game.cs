using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour {

    public static int gridWight = 10;
    public static int gridHeight = 20;

    public float fallSpeed = 1.0f;
    public int scoreOneLine = 5;
    public int scoreTwoLine = 15;
    public int scoreThreeLine = 45;
    public int scoreFourLine = 150;

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
        currentTetrismino = GetRandomTetrismino();

        nextTetrismino = GetRandomTetrismino();

        SpawnNextTetrismino();
             
        audioSource = GetComponent<AudioSource>();
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
        scoreTextJ.text = scoreJ.ToString();
        scoreTextL.text = scoreL.ToString();
        scoreTextS.text = scoreS.ToString();
        scoreTextZ.text = scoreZ.ToString();
        scoreTextSquare.text = scoreSquare.ToString();
        scoreTextT.text = scoreT.ToString();
        scoreTextLong.text = scoreLong.ToString();

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
                randomTetrisminoName = "Prefabs/Tetrismino_t";
                break;
            case 2:
                randomTetrisminoName = "Prefabs/Tetrismino_long";
                break;
            case 3:
                randomTetrisminoName = "Prefabs/Tetrismino_square";
                break;
            case 4:
                randomTetrisminoName = "Prefabs/Tetrismino_j";
                break;
            case 5:
                randomTetrisminoName = "Prefabs/Tetrismino_l";
                break;
            case 6:
                randomTetrisminoName = "Prefabs/Tetrismino_s";
                break;
            case 7:
                randomTetrisminoName = "Prefabs/Tetrismino_z";
                break;
        }
        return randomTetrisminoName;
    }

    public void GameOver ()
    {
        Application.LoadLevel("GameOver");
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
            Application.LoadLevel("MainMenuScene");
        }
    }
}
