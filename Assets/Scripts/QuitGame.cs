using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class QuitGame : MonoBehaviour
{
  public Toggle resetScoreToggle;

  public void StopGame()
  {
    
        if(resetScoreToggle.isOn)
        {
          GameManage.Instance.highScore=0;
          PlayerPrefs.SetInt("HighScore", 0);
          PlayerPrefs.Save(); 
        }
       
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif

  }
}
