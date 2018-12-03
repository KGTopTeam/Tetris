using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tetrismino : MonoBehaviour {

    float fall = 0;

    private float fallSpeed = 1;

    public bool allowRotation = true;
    public bool limitRotation = false;

    private float continoursVerticalSpeed = 0.1f;
    private float continoursHorizontalSpeed = 0.1f;

    private float verticalTimer = 0;
    private float horizotalTimer = 0;

    public AudioClip moveSound;
    public AudioClip rotateSound;
    public AudioClip landSound;

    private AudioSource audioSource;

    public bool isPause = false;


	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        CheckUserInput();
        CheckPause();

        fallSpeed = GameObject.Find("GameScript").GetComponent<Game>().fallSpeed;
    }

    void CheckUserInput () {
        if (!isPause)
        {
            if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.DownArrow))
            {
                horizotalTimer = 0;
                verticalTimer = 0;
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                MoveRight();
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                MoveLeft();
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Rotate();
            }
            if (Input.GetKey(KeyCode.DownArrow) || Time.time - fall >= fallSpeed)
            {
                MoveDown();
            }
        }
    }

    bool CheckIsValidPosition ()
    {
        foreach (Transform mino in transform)
        {
            Vector2 pos = FindObjectOfType<Game>().Round(mino.position);

            if (FindObjectOfType<Game>().CheckInsideGrid (pos) == false)
            {
                return false;
            }

            if (FindObjectOfType<Game>().GetTransformAtGridPosition(pos) != null && FindObjectOfType<Game>().GetTransformAtGridPosition(pos).parent != transform)
            {
                return false;
            }
        }
        return true;
    }

    void PlayMoveAudio()
    {
        audioSource.PlayOneShot(moveSound);
    }

    void PlayRotateAudio()
    {
        audioSource.PlayOneShot(rotateSound);
    }

    void PlayLandAudio()
    {
        audioSource.PlayOneShot(landSound);
    }

    public void CheckPause()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isPause)
            {
                isPause = false;
                FindObjectOfType<Game>().PrintPauseString("");
            }
            else
            {
                isPause = true;
                FindObjectOfType<Game>().PrintPauseString("PAUSE");
            }
        }
    }

    void MoveLeft()
    {

        if (horizotalTimer < continoursHorizontalSpeed)
        {
            horizotalTimer += Time.deltaTime;
            return;
        }

        horizotalTimer = 0;

        transform.position += new Vector3(-1, 0, 0);

        if (CheckIsValidPosition())
        {
            FindObjectOfType<Game>().UpdateGrid(this);
            PlayMoveAudio();
        }
        else
        {
            transform.position += new Vector3(1, 0, 0);
        }
    }

    void MoveRight()
    {
        if (horizotalTimer < continoursHorizontalSpeed)
        {
            horizotalTimer += Time.deltaTime;
            return;
        }

        horizotalTimer = 0;

        transform.position += new Vector3(1, 0, 0);

        if (CheckIsValidPosition())
        {
            FindObjectOfType<Game>().UpdateGrid(this);

            PlayMoveAudio();
        }
        else
        {
            transform.position += new Vector3(-1, 0, 0);
        }
    }

    void Rotate()
    {
        if (allowRotation)
        {
            if (limitRotation)
            {
                if (transform.rotation.eulerAngles.z >= 90)
                {
                    transform.Rotate(0, 0, -90);
                }
                else
                {
                    transform.Rotate(0, 0, 90);
                }
            }
            else
            {
                transform.Rotate(0, 0, 90);
            }
            if (CheckIsValidPosition())
            {
                FindObjectOfType<Game>().UpdateGrid(this);

                PlayRotateAudio();
            }
            else
            {
                if (limitRotation)
                {
                    if (transform.rotation.eulerAngles.z >= 90)
                    {
                        transform.Rotate(0, 0, -90);
                    }
                    else
                    {
                        transform.Rotate(0, 0, 90);
                    }
                }
                else
                {
                    transform.Rotate(0, 0, -90);
                }
            }
        }
    }

    void MoveDown()
    {
        if (verticalTimer < continoursVerticalSpeed)
        {
            verticalTimer += Time.deltaTime;
            return;
        }

        verticalTimer = 0;

        transform.position += new Vector3(0, -1, 0);

        if (CheckIsValidPosition())
        {
            FindObjectOfType<Game>().UpdateGrid(this);

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                PlayMoveAudio();
            }

        }
        else
        {
            transform.position += new Vector3(0, 1, 0);

            FindObjectOfType<Game>().DeleteRow();

            enabled = false;

            if (FindObjectOfType<Game>().CheckIsAboveGrid(this))
            {
                FindObjectOfType<Game>().GameOver();
            }

            PlayLandAudio();

            FindObjectOfType<Game>().SpawnNextTetrismino();
        }

        fall = Time.time;
    }
}
