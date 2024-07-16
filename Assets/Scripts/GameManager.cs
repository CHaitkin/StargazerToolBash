using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool gameRunning;
    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    public GameObject winMenu;
    public static GameManager Instance;
    

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene(2);
        Time.timeScale = 1f;
        gameRunning = true;
    }
    public void Credits()
    {
        SceneManager.LoadScene(3);
    }
    public void GoBack()
    {
        SceneManager.LoadScene(0);
    }

    public void Pause()
    {
        gameRunning = false;
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }
    public void Resume()
    {
        gameRunning = true;
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }

    public void GameOver()
    {
        gameRunning = false;
        gameOverMenu.gameObject.SetActive(true);
    }

    public void TryAgain()
    {
        gameRunning = true;
        SceneManager.LoadScene(2);
    }
    public void Quit()
    {
        SceneManager.LoadScene(0);
    }

    public void ShowWinScreen()
    {
        gameRunning = false;
        winMenu.gameObject.SetActive(true);
    }
    private void Update()
    {
        PlayerController playerController = FindObjectOfType<PlayerController>();
        if(playerController != null)
        {
            gameRunning = true;
        }
        if(playerController == null)
        {
            GameOver();
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            Pause();
        }
    }
}
