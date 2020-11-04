using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tripwire : ElectronicSender
{
    public bool levelExit;

    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement pm = other.GetComponent<PlayerMovement>();
        if (pm)
        {
            SendOn();
            if (levelExit)
                NextScene();
        }
    }

    public void NextScene()
    {
        StartCoroutine(TransitionAfterDuration(1f));
    }

    private IEnumerator TransitionAfterDuration(float t)
    {
        yield return new WaitForSeconds(t);
        int index = SceneManager.GetActiveScene().buildIndex + 1;
        SaveSystem.data.secretLevel |= SaveSystem.unlockSecret;
        SaveSystem.SaveData();
        if (index < SceneManager.sceneCountInBuildSettings - 1)
        {
            if (SaveSystem.data.latestUnlockedLevel < index)
                SaveSystem.UpdateLevels(index);
            SceneManager.LoadScene(index);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
}