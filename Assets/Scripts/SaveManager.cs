using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveManager
{
    public static void SaveStatsData(Player player)
    {
        SaveStatsPlayer saveStatsPlayer = new SaveStatsPlayer(player);
        string dataPath = Application.persistentDataPath + "/player.stats";
        FileStream fileStream = new FileStream(dataPath, FileMode.Create);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(fileStream, saveStatsPlayer);
        fileStream.Close();
    }

    public static SaveStatsPlayer LoadStatsPlayer()
    {
        string dataPath = Application.persistentDataPath + "/player.stats";
        if (File.Exists(dataPath))
        {
            FileStream fileStream = new FileStream(dataPath, FileMode.Open);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            SaveStatsPlayer  saveStatsPlayer = (SaveStatsPlayer) binaryFormatter.Deserialize(fileStream);
            fileStream.Close();
            return saveStatsPlayer;
        }
        else
        {
            Debug.LogError("El archivo no fue encontrado");
            return null;
        }
    }
}
