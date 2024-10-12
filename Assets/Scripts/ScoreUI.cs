using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  
public class ScoreUI : MonoBehaviour
{
     public TextMeshProUGUI scoreText; 
    private void Update()
    {
        if (GameManage.Instance != null)
        {
            scoreText.text = "Score: " + GameManage.Instance.score.ToString() + "   Lives: " 
            + GameManage.Instance.lives.ToString() + "  High Score: " + GameManage.Instance.highScore.ToString();
        }
    }
}
