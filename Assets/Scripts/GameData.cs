using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int latestUnlockedLevel;
    public int deaths;
    public float volume;
    public float sensitivity;
    public bool secretLevel;

    public GameData()
    {
    }
}
