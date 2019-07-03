using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image[] playerLivesDisplay;
    public Image titleScreen;
    public Text instructions;
    public Text scoreText;
    public Text topScoreText;
    public int totalScore = 0;
    public int topScore = 0;

    private void Start()
    {
        totalScore = 0;
        topScore = PlayerPrefs.GetInt("TopScore", 0);
        topScoreText.text = ("Top Score: " + topScore);
    }

    public void UpdateLives(int currentLives)
    {
        switch (currentLives)
        {
            case 3:
                playerLivesDisplay[2].enabled = true;
                playerLivesDisplay[1].enabled = true;
                playerLivesDisplay[0].enabled = true;
                break;
            case 2:
                playerLivesDisplay[2].enabled = false;
                break;
            case 1:
                playerLivesDisplay[1].enabled = false;
                break;
            case 0:
                playerLivesDisplay[0].enabled = false;
                break;
            default:
                Debug.Log("This case should never happen!");
                break;
        }
    }

    public void UpdateScore(int score)
    {
        totalScore = totalScore + score;
        scoreText.text = "Score: " + totalScore;
    }

    public void CheckForTopScore()
    {
        if (totalScore > topScore)
        {
            topScore = totalScore;
            topScoreText.text = "Top Score: " + topScore;
            PlayerPrefs.SetInt("TopScore", topScore);
            PlayerPrefs.Save();
        }
    }

    public void ShowTitleScreen()
    {
        titleScreen.enabled = true;
    }

    public void HideTitleScreen()
    {
        titleScreen.enabled = false;
    }

    public void ShowInstructions()
    {
        instructions.enabled = true;
    }

    public void HideInstructions()
    {
        instructions.enabled = false;
    }

    public void ExitToMainMenu()
    {
        Debug.Log("Main Menu is loading...");
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void ReturnToGame()
    {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.ResumeGame();
    }
}
