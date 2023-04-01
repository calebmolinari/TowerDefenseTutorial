using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    
    public static void SavePlayer(PlayerData playerData)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        SavePlayer data = new SavePlayer(playerData);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SavePlayer LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.save";
        if (File.Exists(path)) 
        { 
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SavePlayer data = formatter.Deserialize(stream) as SavePlayer;
            stream.Close();

            return data;
        } else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

    public static void SaveOverworld(GameObject player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/overworld.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveOverworld data = new SaveOverworld(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SaveOverworld LoadOverworld()
    {
        string path = Application.persistentDataPath + "/overworld.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveOverworld data = formatter.Deserialize(stream) as SaveOverworld;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

    public static void SaveUnlocks(TurretUnlockData unlockData)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/unlocks.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveUnlocks data = new SaveUnlocks(unlockData);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SaveUnlocks LoadUnlocks()
    {
        string path = Application.persistentDataPath + "/unlocks.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveUnlocks data = formatter.Deserialize(stream) as SaveUnlocks;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
