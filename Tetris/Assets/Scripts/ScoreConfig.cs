using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ScoreSettings", menuName = "ScoreSettings")]
public class ScoreConfig : ScriptableObject {

    public int pointForOneLines;

    public int pointForTwoLines;

    public int pointForThreeLines;

    public int pointForFourLines;

    public int getPointForOneLines()
    {
        return pointForOneLines;
    }

    public int getPointForTwoLines()
    {
        return pointForTwoLines;
    }

    public int getPointForThreeLines()
    {
        return pointForThreeLines;
    }

    public int getPointForFourLines()
    {
        return pointForFourLines;
    }
}
