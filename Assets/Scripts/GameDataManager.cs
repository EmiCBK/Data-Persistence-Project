using System.IO;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance { get; private set; }

    public string PlayerName = "";
    public string BestPlayerName = "Name";
    public int BestScore = 0;

    private string SavePath =>
        Path.Combine(Application.persistentDataPath, "highscore.json");

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadHighScore();
    }

    public void UpdateHighScore(int score)
    {
        if (score <= BestScore)
            return;

        BestScore = score;
        BestPlayerName = PlayerName;

        SaveData data = new SaveData
        {
            BestPlayerName = BestPlayerName,
            BestScore = BestScore
        };

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(SavePath, json);
    }

    private void LoadHighScore()
    {
        if (!File.Exists(SavePath))
            return;

        string json = File.ReadAllText(SavePath);
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        if (data == null)
            return;

        BestPlayerName = data.BestPlayerName;
        BestScore = data.BestScore;
    }

    [System.Serializable]
    private class SaveData
    {
        public string BestPlayerName;
        public int BestScore;
    }
}