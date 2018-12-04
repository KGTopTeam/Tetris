using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour {

    public Toggle allowZ;
    public Toggle allowT;
    public Toggle allowS;
    public Toggle allowL;
    public Toggle allowJ;
    public Toggle allowSquare;
    public Toggle allowLong;

    public FormsConfig formsConfig;

    public Text outputOne;
    public Text outputTwo;
    public Text outputThree;
    public Text outputFour;

    public ScoreConfig scoreConfig;

    public Slider inputOne;
    public Slider inputTwo;
    public Slider inputThree;
    public Slider inputFour;

    public void Start()
    {
        if (formsConfig != null)
        {
            if (allowZ != null)
            {
                allowZ.isOn = formsConfig.allowZ;
            }
            if (allowS != null)
            {
                allowS.isOn = formsConfig.allowS;
            }
            if (allowL != null)
            {
                allowL.isOn = formsConfig.allowL;
            }
            if (allowJ != null)
            {
                allowJ.isOn = formsConfig.allowJ;
            }
            if (allowLong != null)
            {
                allowZ.isOn = formsConfig.allowLong;
            }
            if (allowSquare != null)
            {
                allowSquare.isOn = formsConfig.allowSquare;
            }
            if (allowT != null)
            {
                allowT.isOn = formsConfig.allowT;
            }
        }

        if (scoreConfig != null)
        {
            if (outputOne != null)
            {
                outputOne.text = scoreConfig.pointForOneLines.ToString();

                if (inputOne != null)
                {
                    inputOne.value = (float)scoreConfig.pointForOneLines;
                }
            }
            if (outputTwo != null)
            {
                outputTwo.text = scoreConfig.pointForTwoLines.ToString();

                if (inputTwo != null)
                {
                    inputTwo.value = (float)scoreConfig.pointForTwoLines;
                }
            }
            if (outputThree != null)
            {
                outputThree.text = scoreConfig.pointForThreeLines.ToString();

                if (inputThree != null)
                {
                    inputThree.value = (float)scoreConfig.pointForThreeLines;
                }
            }
            if (outputFour != null)
            {
                outputFour.text = scoreConfig.pointForFourLines.ToString();

                if (inputFour != null)
                {
                    inputFour.value = (float)scoreConfig.pointForFourLines;
                }
            }
        }
    }

    public void SetAllowT (bool value)
    {
        if(formsConfig != null)
        {
            formsConfig.allowT = value;
        }
    }

    public void SetAllowL(bool value)
    {
        if (formsConfig != null)
        {
            formsConfig.allowL = value;
        }
    }

    public void SetAllowLong(bool value)
    {
        if (formsConfig != null)
        {
            formsConfig.allowLong = value;
        }
    }

    public void SetAllowSquare(bool value)
    {
        if (formsConfig != null)
        {
            formsConfig.allowSquare = value;
        }
    }

    public void SetAllowS(bool value)
    {
        if (formsConfig != null)
        {
            formsConfig.allowS = value;
        }
    }

    public void SetAllowZ(bool value)
    {
        if (formsConfig != null)
        {
            formsConfig.allowZ = value;
        }
    }

    public void SetAllowJ(bool value)
    {
        if (formsConfig != null)
        {
            formsConfig.allowJ = value;
        }
    }

    public void SetPointOne(float value)
    {
        if (scoreConfig != null)
        {
            scoreConfig.pointForOneLines = (int)value;

            if (outputOne != null)
            {
                outputOne.text = ((int)value).ToString();
            }
        }
    }

    public void SetPointTwo(float value)
    {
        if (scoreConfig != null)
        {
            scoreConfig.pointForTwoLines = (int)value;

            if (outputTwo != null)
            {
                outputTwo.text = ((int)value).ToString();
            }
        }
    }

    public void SetPointThree(float value)
    {
        if (scoreConfig != null)
        {
            scoreConfig.pointForThreeLines = (int)value;

            if (outputThree != null)
            {
                outputThree.text = ((int)value).ToString();
            }
        }
    }

    public void SetPointFour(float value)
    {
        if (scoreConfig != null)
        {
            scoreConfig.pointForFourLines = (int)value;

            if (outputFour != null)
            {
                outputFour.text = ((int)value).ToString();
            }
        }
    }
}
