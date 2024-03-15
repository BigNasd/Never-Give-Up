using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager :BaseManager<SaveManager>
{
    public void JsonSave(string fileName, object data)
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(GetPath(fileName), json);

        Debug.Log($"“—±£¥Ê{GetPath(fileName)}");
    }

    private string GetPath(string fileName)
    {
        return Path.Combine(Application.persistentDataPath, fileName);
    }

    public T JsonLoad<T>(string fileName)
    {
        string path = GetPath(fileName);
        if (File.Exists(path))
        {
            string json = File.ReadAllText(GetPath(fileName));
            T data = JsonUtility.FromJson<T>(json);
            Debug.Log($"∂¡»°{path}");
            return data;
        }
        else
        {
            return default;
        }
    }

    public void JosnDelete(string fileName)
    {
        File.Delete(GetPath(fileName));
    }
}
