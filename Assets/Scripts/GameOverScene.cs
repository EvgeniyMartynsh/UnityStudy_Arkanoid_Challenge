using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScene : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI bestScoreText;
    [SerializeField] TextMeshProUGUI playerScoreText;
    [SerializeField] public static int playerScore;


    // Start is called before the first frame update
    void Start()
    {
        SetScore(playerScore);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(1);
            MainManager.isNewHighScore = false;

        }
    }

    void SetScore(int currentScore)
    {

        if (MainManager.isNewHighScore)
        {
            bestScoreText.text = "New High Score!!! You score: " + currentScore;
            playerScoreText.text = "Previous best High Score: " + MainManager.lastBestScore;
        }

        else
        {
            bestScoreText.text = "Best player: " + SaveManager.Instance.HighScorePlayerName +
            " " + SaveManager.Instance.HighScore;

            playerScoreText.text = "You score: " + currentScore;
        }

    }
}
