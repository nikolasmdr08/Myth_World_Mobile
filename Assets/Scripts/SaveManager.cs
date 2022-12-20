using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveManager
{
    public static void SaveStatsData<T>(T obj, string url)
    {
        SaveStatsPlayer <T>saveStatsPlayer = new SaveStatsPlayer<T>(obj);
        string dataPath = Application.persistentDataPath + "/"+ url;
        FileStream fileStream = new FileStream(dataPath, FileMode.Create);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(fileStream, saveStatsPlayer);
        fileStream.Close();
    }

    public static T LoadStatsPlayer<T>(string url)
    {
        T saveStatsPlayer;
        string dataPath = Application.persistentDataPath + "/" + url;
        FileStream fileStream = new FileStream(dataPath, FileMode.Open);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        saveStatsPlayer = (T) binaryFormatter.Deserialize(fileStream);
        fileStream.Close();
        return saveStatsPlayer;
    }
}
