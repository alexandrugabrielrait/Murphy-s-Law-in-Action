using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : Menu
{
    public static bool gamePaused;
    public GameObject pauseMenuUI;

    private float timeScale = 1f;

    private void Start()
    {
        Resume();
    }

    private void Update()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            if (!FindObjectOfType<PlayerMovement>().isDead)
            {
                if (gamePaused)
                    Resume();
                else
                    Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = timeScale;
        gamePaused = false;
    }


    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        timeScale = Time.timeScale;
        Time.timeScale = 0f;
        gamePaused = true;
    }

    public void Stop()
    {
        Time.timeScale = 0f;
        gamePaused = true;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        if (!FindObjectOfType<PlayerMovement>().isDead)
        {
            ++SaveSystem.data.deaths;
            SaveSystem.SaveData();
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
