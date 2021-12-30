using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public IntScriptableObject scoreSO;
    public Text scoreText;
    public TextMeshProUGUI playPauseButtonText;

    private PlayerInput playerInput;
    private InputAction playPause;

    private void Update() 
    {
        scoreText.text = "SCORE: " + scoreSO.value;
    }

    public void playPauseTask()
    {
        if (Time.timeScale != 0)
        {
            Time.timeScale = 0;
            playPauseButtonText.text = "PLAY";
        }
        else 
        {
            Time.timeScale = 1;
            playPauseButtonText.text = "PAUSE";
        }
        
    }

    public void restartTask()
    {
        SceneManager.LoadScene("MainScene");
        if (Time.timeScale == 0)  // if the game is paused restart it
        {
            playPauseTask();
        }
    }

    public void quitTask()
    {
        Application.Quit();
    }
}
