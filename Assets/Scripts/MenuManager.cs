using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;


public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    public string PlayerName;
    private int highScore;
    private string highScorePlayer;
    public string HighScorePlayer {get {return highScorePlayer;}}

    public int HighScore
    {
        get { return this.highScore;}
        set {
            if (value > this.highScore)
            {
                this.highScore = value;
                this.highScorePlayer = Instance.PlayerName;
            }
        }
    }

    private void Awake() 
    {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(gameObject);
            return;
        }
        LoadHighScore();
    }

    [System.Serializable]
    class SaveData
    {
        public string Player;
        public string HighScorePlayer;
        public int HighScore;
        
    }
    public void SaveHighscore()
    {
        SaveData data = new SaveData();
        data.Player = this.PlayerName;
        data.HighScorePlayer = this.HighScorePlayer;
        data.HighScore = this.HighScore;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/save_persistent_file.json", json);
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/save_persistent_file.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            this.PlayerName = data.Player;
            this.highScore = data.HighScore;
            this.highScorePlayer = data.HighScorePlayer;
        }
    }

}
