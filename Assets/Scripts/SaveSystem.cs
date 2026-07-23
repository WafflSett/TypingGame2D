using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem<T> where T : Object
{
    public static string path = Application.persistentDataPath+"/saves/";

    public static void SaveData(T data, string fileName)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        using FileStream stream = new FileStream(path + fileName, FileMode.Create);
        formatter.Serialize(stream, data);
    }

    public static T LoadData(string fileName)
    {
        if (!File.Exists(path+fileName))
        {
            Debug.Log("Error: Savefile not found at " + path + fileName);
            return null;
        }
        BinaryFormatter formatter = new BinaryFormatter();
        using FileStream stream = new FileStream(path + fileName, FileMode.Open);
        return formatter.Deserialize(stream) as T;
    }
}
