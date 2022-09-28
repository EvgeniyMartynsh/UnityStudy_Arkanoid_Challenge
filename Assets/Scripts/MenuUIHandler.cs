using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MenuUIHandler : MonoBehaviour

{

    [SerializeField] GameObject inputNameField;
    [SerializeField] TextMeshProUGUI textBestScore;

    public static string playerName;



    // Start is called before the first frame update
    void Start()
    {
        textBestScore.text = "Best score: " + SaveManager.Instance.HighScorePlayerName + " - " +
            SaveManager.Instance.HighScore;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);

        playerName = inputNameField.GetComponent<TMP_InputField>().text;
            if (string.IsNullOrEmpty(playerName))
            {
                playerName = "Noname_player";
            }
        
       // Debug.Log("PlayerName: " + playerName);
    }

    public void Exit()
    {
    #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
    #else
            Application.Quit();
    #endif

    }
}
