using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    const string fileName = "save.json";

    public static void Save(int coins, int bowLevel = 0, int spearLevel = 0)
    {
        var save = Load();
        save.Coins += coins;
        if(bowLevel > 0)
            save.BowLevel = bowLevel;
        if (spearLevel > 0)
            save.SpearLevel = spearLevel;
        
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
