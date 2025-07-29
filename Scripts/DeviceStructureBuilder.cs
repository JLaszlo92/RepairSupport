using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEditor;

public class DeviceStructureBuilder
{
    private const string devicesFolder = "Assets/Resources/Data/Devices";
    private const string outputPath = "Assets/Resources/Data/deviceSceneMap.json";

    [MenuItem("Tools/Build Device Scene Map")]
    public static void BuildStructure()
    {
        if (!Directory.Exists(devicesFolder))
        {
            Debug.LogError("Devices folder not found: " + devicesFolder);
            return;
        }

        var structure = new Dictionary<string, List<string>>();

        var deviceFolders = Directory.GetDirectories(devicesFolder);
        foreach (var devicePath in deviceFolders)
        {
            string deviceName = Path.GetFileName(devicePath);
            var sceneFolders = Directory.GetDirectories(devicePath)
                                        .Select(Path.GetFileName)
                                        .ToList();
            structure[deviceName] = sceneFolders;
        }

        string json = JsonUtility.ToJson(new SerializableMap(structure), true);
        File.WriteAllText(outputPath, json);
        AssetDatabase.Refresh();
        Debug.Log("✅ Device scene structure saved to: " + outputPath);
    }

   [System.Serializable]
public class StringListWrapper
{
    public List<string> list = new();
}

[System.Serializable]
public class SerializableMap
{
    public List<string> keys = new();
    public List<StringListWrapper> values = new();

    public SerializableMap(Dictionary<string, List<string>> dict)
    {
        foreach (var kv in dict)
        {
            keys.Add(kv.Key);
            values.Add(new StringListWrapper { list = kv.Value });
        }
    }

    public Dictionary<string, List<string>> ToDictionary()
    {
        var result = new Dictionary<string, List<string>>();
        for (int i = 0; i < keys.Count; i++)
        {
            result[keys[i]] = values[i].list;
        }
        return result;
    }
}

}
