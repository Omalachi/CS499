using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class GameResult
{
    public int die1;
    public int die2;
    public int total;
    public string outcome;
    public int betAmount;
}

[System.Serializable]
public class GameResultList
{
    public List<GameResult> results = new List<GameResult>();
}

public class DataManager : MonoBehaviour
{
    private string filePath;
    private GameResultList gameResults = new GameResultList();

    void Awake()
    {
        filePath = Path.Combine(Application.persistentDataPath, "craps_game_results.json");
        LoadData();
    }

    public void LogGameResult(int die1, int die2, int total, string outcome, int betAmount)
    {
        GameResult newResult = new GameResult
        {
            die1 = die1,
            die2 = die2,
            total = total,
            outcome = outcome,
            betAmount = betAmount
        };

        gameResults.results.Add(newResult);
        SaveData();
    }

    private void SaveData()
    {
        string json = JsonUtility.ToJson(gameResults, true);
        File.WriteAllText(filePath, json);
        Debug.Log("✅ Game result saved to JSON at: " + filePath);
    }

    private void LoadData()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            gameResults = JsonUtility.FromJson<GameResultList>(json);
            Debug.Log("✅ Game data loaded from file.");
        }
        else
        {
            Debug.Log("ℹ️ No game results file found. Starting fresh.");
            gameResults = new GameResultList();
        }
    }
}
