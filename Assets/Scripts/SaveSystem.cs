using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static GameData data = new GameData();
    public static bool unlockSecret = false;

    public static void UpdateLevels(int i)
    {
        data.latestUnlockedLevel = i;
        SaveData();
    }

    public static void SaveData()
    {
        string path = Application.persistentDataPath + "/levels.dat";
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void LoadData()
    {
        string path = Application.persistentDataPath + "/levels.dat";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            data = formatter.Deserialize(stream) as GameData;
            stream.Close();
        }
        else
        {
            data.latestUnlockedLevel = 1;
            data.deaths = 0;
            data.volume = 1;
            data.sensitivity = 100;
            data.secretLevel = false;
        }
    }
}