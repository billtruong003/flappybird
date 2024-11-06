using System.Collections;
using System.Collections.Generic;
using System.IO;
using Flappy.Data;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private GameObject containerTitle;
    [SerializeField] private GameObject gameOverContainer;
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TextMeshProUGUI highestScore;

    private const string fileName = "bestScore.json";
    
    private void Start()
    {
        Instance = this;
        LoadBestScore();
        GameStart();
    }

    public void GameStart()
    {
        if (GameManager.Instance.GameState == GameState.MainMenu)
        {
            containerTitle.SetActive(true);
            gameOverContainer.SetActive(false);
        }
        else
        {
            containerTitle.SetActive(false);
        }
    }

    public void GameOver()
    {
        gameOverContainer.SetActive(true);
    }
    public void UpdateCurrentScore(int currentScore)
    {
        score.text = $"Score: {currentScore}";
    }

    public void UpdateBestScore(int currentScore)
    {
        int bestScore = GetBestScore();
        if (currentScore > bestScore)
        {
            highestScore.text = $"Best: {currentScore}";
            SaveBestScore(currentScore);
        }
    }

    private void SaveBestScore(int score)
    {
        BestScoreData data = new BestScoreData { highestScore = score };
        string json = JsonUtility.ToJson(data);

        string path = Path.Combine(Application.persistentDataPath, fileName);
        File.WriteAllText(path, json);
        Debug.Log($"Best score saved to {path}");
    }

    private void LoadBestScore()
    {
        string path = Path.Combine(Application.persistentDataPath, fileName);
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            BestScoreData data = JsonUtility.FromJson<BestScoreData>(json);
            highestScore.text = $"Best: {data.highestScore}";
        }
        else
        {
            highestScore.text = "Best: 0";
            Debug.Log("Best score file not found, initializing to 0.");
        }
    }

    public int GetBestScore()
    {
        string path = Path.Combine(Application.persistentDataPath, fileName);
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            BestScoreData data = JsonUtility.FromJson<BestScoreData>(json);
            return data.highestScore;
        }
        return 0;
    }
}
