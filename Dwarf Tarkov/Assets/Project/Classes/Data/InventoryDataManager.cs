using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class InventoryDataManager : SaveLoadManager<List<ItemDTO>>
{
    public BinaryFormatter GetBinaryFormatter()
    {
        return new BinaryFormatter();
    }

    public List<ItemDTO> Load()
    {
        BinaryFormatter formatter = GetBinaryFormatter();
        string path = $"{Application.persistentDataPath}/saves/dateData.gos";

        if (!File.Exists(path))
        {
            return null;
        }

        FileStream file = File.Open(path, FileMode.Open);

        try
        {
            var save = formatter.Deserialize(file) as List<ItemDTO>;
            file.Close();
            return save;
        }
        catch
        {
            Debug.LogError("Could not find save");
            return null;
        }
    }

    public void Save(List<ItemDTO> data)
    {
        BinaryFormatter formatter = GetBinaryFormatter();
        if (!Directory.Exists($"{Application.persistentDataPath}/saves"))
        {
            Directory.CreateDirectory($"{Application.persistentDataPath}/saves");
        }

        string path = $"{Application.persistentDataPath}/saves/inventory.dk";

        FileStream file = File.Create(path);

        formatter.Serialize(file, data);

        file.Close();
    }
}
