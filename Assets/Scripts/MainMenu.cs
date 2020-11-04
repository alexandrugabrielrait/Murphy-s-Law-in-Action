using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MainMenu : Menu
{
    public GameObject mainMenuUI;
    public GameObject levelSelectMenuUI;
    public GameObject optionsMenuUI;
    public Slider volumeSlider;
    public Slider sensitivitySlider;
    public TextMeshProUGUI newGameText;
    public Button levelSelectButton;
    public Button resetButton;
    public Button[] levelButtons;
    public Button secretLevelButton;

    public void Awake()
    {
        levelSelectMenuUI.SetActive(false);
        optionsMenuUI.SetActive(false);
        mainMenuUI.SetActive(true);
        PauseMenu.gamePaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SaveSystem.LoadData();
        volumeSlider.value = SaveSystem.data.volume;
        sensitivitySlider.value = SaveSystem.data.sensitivity;
        if (SaveSystem.data.latestUnlockedLevel > 1)
            newGameText.text = "Continue";
        else
            newGameText.text = "New Game";
        if (SaveSystem.data.latestUnlockedLevel == 1)
        {
            levelSelectButton.enabled = false;
            resetButton.enabled = false;
        }
        else
        {
            levelSelectButton.enabled = true;
            resetButton.enabled = true;
            for (int i = SaveSystem.data.latestUnlockedLevel; i < levelButtons.Length; ++i)
                levelButtons[i].gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            SaveSystem.UpdateLevels(5);
            Awake();
            return;
        }
        else if (Input.GetKeyDown(KeyCode.Minus))
        {
            SaveSystem.data.secretLevel = true;
            SaveSystem.SaveData();
            Awake();
            return;
        }
        volumeSlider.GetComponentInChildren<TMP_Text>().text = "Volume: " +
            Mathf.RoundToInt(volumeSlider.value * 100) + "%";
        sensitivitySlider.GetComponentInChildren<TMP_Text>().text = "Mouse Sensitivity: " +
            Mathf.RoundToInt(sensitivitySlider.value) + "%";
    }

    public void Continue()
    {
        mainMenuUI.SetActive(false);
        StartTransition(SaveSystem.data.latestUnlockedLevel);
    }

    public void SelectLevel(int id)
    {
        levelSelectMenuUI.SetActive(false);
        StartTransition(id);
    }

    public void LevelSelectMenu()
    {
        mainMenuUI.SetActive(false);
        levelSelectMenuUI.SetActive(true);
        secretLevelButton.gameObject.SetActive(SaveSystem.data.secretLevel);
    }

    public void OptionsMenu()
    {
        mainMenuUI.SetActive(false);
        optionsMenuUI.SetActive(true);
    }

    public void Back()
    {
        Awake();
    }

    public void StartTransition(int id)
    {
        Time.timeScale = 1;
        PauseMenu.gamePaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        StartCoroutine(TransitionAfterDuration(id, 0.2f));
    }
    public void OptionsReset()
    {
        resetButton.enabled = false;
        SaveSystem.data.deaths = 0;
        SaveSystem.data.secretLevel = false;
        SaveSystem.UpdateLevels(1);
    }

    private IEnumerator TransitionAfterDuration(int id, float t)
    {
        yield return new WaitForSeconds(t);
        if (id == 0)
            SceneManager.LoadScene("Playground");
        else
            SceneManager.LoadScene(id);
    }

    public void ForceAudioPlaySample()
    {
        if (optionsMenuUI.activeSelf)
        {
            AudioManager.instance.SetVolume(volumeSlider.value);
            AudioManager.instance.PlaySample();
        }
    }
    public void ForceSensitivity()
    {
        if (optionsMenuUI.activeSelf)
        {
            SaveSystem.data.sensitivity = sensitivitySlider.value;
            SaveSystem.SaveData();
        }
    }

    public void SetDefaultVolume()
    {
        volumeSlider.value = 1;
    }

    public void SetDefaultSensitivity()
    {
        sensitivitySlider.value = 100;
    }
}