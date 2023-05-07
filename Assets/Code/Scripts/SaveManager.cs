using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    const string fileName = "save.json";

    public static void Save(int coins)
    {
        var save = Load();
        save.Coins += coins;
        
        DumpToFile(save);
    }

    public static Save Load()
    {
        if (!File.Exists(fileName))
            CreateFile();

        var dict = JsonUtility.FromJson<Save>(File.ReadAllText(fileName));

        return dict;
    }

    static void CreateFile()
    {   
        DumpToFile(new());
    }

    static void DumpToFile(Save save)
    {
        string saveString = JsonUtility.ToJson(save);
        Debug.Log(saveString);
        File.WriteAllText(fileName, saveString);
    }
}
