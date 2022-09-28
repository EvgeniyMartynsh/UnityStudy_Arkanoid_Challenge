using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    
    public static SaveManager Instance;

    public string HighScorePlayerName;
    public int HighScore;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Load();
    }

    [System.Serializable]

    class SaveData
    {
        public string HighScorePlayerName;
        public int HighScore;
    }


    public void Save()
    {
        SaveData data = new SaveData();
        
        data.HighScorePlayerName = HighScorePlayerName;
        data.HighScore = HighScore;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        Debug.Log("Save" + json);
    }

    public void Load()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            HighScore = data.HighScore;
            HighScorePlayerName = data.HighScorePlayerName;
        }
    }
}
