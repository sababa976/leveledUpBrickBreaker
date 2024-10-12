using UnityEngine;
using System.Collections;

using UnityEngine.SceneManagement;

public class GameManage : MonoBehaviour
{
    public BallScript ball {get; set;}
    public PaddleController paddle {get; private set;}
    public static GameManage Instance { get; private set; }
    public int score = 0;
    public int highScore;
    public int currentLevel = 1;
    public const int maxLevel = 2;

    public BrickScript[] Bricks {get; private set;}

    public int lives = 3;

    private void Awake()
    {
        // if (Instance != null && Instance != this)
        // {
        //     Destroy(this.gameObject);
        // }
        // else
        // {
        //     Instance = this;
        //     DontDestroyOnLoad(this.gameObject);
        // }
        // //SceneManager.LoadScene("StartGameScene");
        // //NewGame();
        // //SceneManager.LoadScene("StartGameScene");
        //  SceneManager.sceneLoaded += OnLevelLoad;

        // // Load the starting scene
        // SceneManager.LoadScene("StartGameScene");
         // Prevent duplication of GameManage object
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return; // Exit the method if a duplicate instance is found
        }
        
        Instance = this;
        DontDestroyOnLoad(this.gameObject); // Prevent destruction on scene load

        SceneManager.sceneLoaded += OnLevelLoad;

        NewGame();
            }
    private void Start()
    {        
    }
    private void OnLevelLoad(Scene scene,LoadSceneMode mode)
    {
        ball = FindObjectOfType<BallScript>();
        paddle = FindObjectOfType<PaddleController>();
        Bricks = FindObjectsOfType<BrickScript>();       
    }

    public void NewGame()
    {
        score = 0;
        currentLevel = 1;
        lives = 3;
        highScore = PlayerPrefs.GetInt("HighScore", 0); // Retrieve high score
        StartNewLevel(1);
    }
    public void StartNewLevel(int level)
    {
        if(level==-1)
        {
            SceneManager.LoadScene("GameWon");
        }
        else
        {
            SceneManager.LoadScene("Level"+level);
        }

    }
    public void AddScore(int ScoreToAdd)
    {
        score += ScoreToAdd;
        //updating high score if needed
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
            Debug.Log("New High Score saved: " + score);
        }
        //if level is cleared from all bricks
        if(cleared())
        {
            //game won
            if(currentLevel == maxLevel)
                StartNewLevel(-1);
            else
            {
                currentLevel++;
                StartNewLevel(currentLevel);
            }
        }
       
    }
    //checking if there are any bricks left to destroy in level
    private bool cleared()
    {
        for(int i=0;i<this.Bricks.Length;i++)
        {
            if(this.Bricks[i].gameObject.activeInHierarchy)
            {
                return false;
            }
        }
        return true;
    }
    private void ResetLevel()
    {
        ball.ResetBall();
        paddle.ResetPaddle();
    }
    private void GameLost()
    {
        SceneManager.LoadScene("GameOver");
    }
    public void LoseLife()
    {
        lives--;
        if(lives>0)
        {
            ResetLevel();
        }
        else
        {
            GameLost();
        }
    }
}
