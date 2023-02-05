using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class DataSaver<T> where T : class
{
    private static string path = $"{Application.persistentDataPath}/Dwarkov/";
    public static void Save(T data, string name)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream($"{path}/{name}.dk", FileMode.Create);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static T Load(string name)
    {
        if (!File.Exists($"{path}/{name}.dk"))
        {
            Debug.Log($"No {name} data exists!");
            return default(T);
        }

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream($"{path}/{name}.dk", FileMode.Open);
        T data = (T)formatter.Deserialize(stream);
        Debug.Log($"Loaded {name} data successfully");
        stream.Close();
        return data;
    }
}
