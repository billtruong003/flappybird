using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Flappy.Data;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState GameState;
    public FlappyController playerController;
    private int score; // Biến lưu trữ điểm hiện tại

    void Start()
    {
        Instance = this;
        GameState = GameState.MainMenu;
        playerController.Init(GameState);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (GameState == GameState.MainMenu)
            {
                GameState = GameState.Playing;
                score = 0; 
                playerController.Init(GameState);
                UIManager.Instance.GameStart();
                UpdateScoreUI(); 
            }
            else if (GameState == GameState.GameOver)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    public void GameOver()
    {
        GameState = GameState.GameOver;
        UIManager.Instance.GameOver();
    }
    public void IncreaseScore(int amount)
    {
        if (GameState == GameState.Playing)
        {
            score += amount;
            UpdateScoreUI();
            CheckBestScore();
        }
    }

    private void UpdateScoreUI()
    {
        UIManager.Instance.UpdateCurrentScore(score); // Gọi UIManager để cập nhật UI
    }

    private void CheckBestScore()
    {
        if (score > UIManager.Instance.GetBestScore())
        {
            UIManager.Instance.UpdateBestScore(score); // Gọi để lưu điểm cao nhất
        }
    }
}
