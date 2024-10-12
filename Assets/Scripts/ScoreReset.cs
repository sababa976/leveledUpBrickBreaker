using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreReset : MonoBehaviour
{
    public void ResetTheHighScore()
    {
        GameManage.Instance.highScore=0;
        PlayerPrefs.SetInt("HighScore", 0);
        PlayerPrefs.Save(); 
    }
}
