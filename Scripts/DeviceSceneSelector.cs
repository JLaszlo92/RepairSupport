using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class DeviceSceneDropdown : MonoBehaviour
{
    public TMP_Dropdown deviceDropdown;
    public TMP_Dropdown sceneDropdown;



    private Dictionary<string, List<string>> deviceSceneMap;

    [System.Serializable]
    public class ListWrapper
    {
        public List<string> list;
    }

    [System.Serializable]
    public class SerializableMap
    {
        public List<string> keys = new List<string>();
        public List<ListWrapper> values = new List<ListWrapper>();

        public Dictionary<string, List<string>> ToDictionary()
        {
            var dict = new Dictionary<string, List<string>>();
            for (int i = 0; i < keys.Count; i++)
            {
                if (values.Count <= i || values[i] == null || values[i].list == null)
                {
                    Debug.LogError($"❌ Invalid data at index {i}");
                    continue;
                }

                dict[keys[i]] = values[i].list;
            }
            return dict;
        }
    }

    void Awake()
    {
        // Load JSON from Resources
        TextAsset jsonText = Resources.Load<TextAsset>("Data/deviceSceneMap");
        if (jsonText == null)
        {
            Debug.LogError("❌ deviceSceneMap.json not found in Resources/Data/");
            return;
        }

        SerializableMap map = JsonUtility.FromJson<SerializableMap>(jsonText.text);
        deviceSceneMap = map.ToDictionary();

        PopulateDeviceDropdown();
    }

    void PopulateDeviceDropdown()
    {
        deviceDropdown.ClearOptions();
        deviceDropdown.AddOptions(new List<string>(deviceSceneMap.Keys));

        // Add listener
        deviceDropdown.onValueChanged.AddListener(OnDeviceChanged);

        // Initialize scene dropdown
        if (deviceDropdown.options.Count > 0)
        {
            OnDeviceChanged(0);
        }
    }

    void OnDeviceChanged(int index)
    {
        string selectedDevice = deviceDropdown.options[index].text;
        List<string> scenes = deviceSceneMap[selectedDevice];

        sceneDropdown.ClearOptions();
        sceneDropdown.AddOptions(scenes);
    }
}
