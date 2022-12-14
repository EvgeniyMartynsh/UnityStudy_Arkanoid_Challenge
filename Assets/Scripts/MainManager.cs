using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText, bestScoreText;

    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;
    public static bool isNewHighScore;
    public static int lastBestScore;

    
    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

        SetBestScoreText();

    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }

    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        SaveBestScorePlayer();
        GameOverScene.playerScore = m_Points;
        SceneManager.LoadScene(2);
    }

    void SetBestScoreText()
    {
        if (string.IsNullOrEmpty(SaveManager.Instance.HighScorePlayerName))
        {
            bestScoreText.text = "Best score: no best score";
        }

        else
        {
            bestScoreText.text = "Best Player: " + SaveManager.Instance.HighScorePlayerName + " score: " + SaveManager.Instance.HighScore;
        }
    }

    void SaveBestScorePlayer()
    {
        if (m_Points > SaveManager.Instance.HighScore)
        {
            lastBestScore = SaveManager.Instance.HighScore;
            isNewHighScore = true;

            SaveManager.Instance.HighScore = m_Points;
            SaveManager.Instance.HighScorePlayerName = MenuUIHandler.playerName;
            SaveManager.Instance.Save();
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        SaveBestScorePlayer();
    }
}
