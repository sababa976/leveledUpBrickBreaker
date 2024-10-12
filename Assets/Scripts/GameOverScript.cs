using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{
      public Toggle resetScoreToggle;
    public void RestartGame()
    {
        if(resetScoreToggle.isOn)
        {
          GameManage.Instance.highScore=0;
          PlayerPrefs.SetInt("HighScore", 0);
          PlayerPrefs.Save();
        }
        if (GameManage.Instance != null)
        {
            GameManage.Instance.NewGame();
        }
        else
        {
            SceneManager.LoadScene("Global");
        }
    }   
}
