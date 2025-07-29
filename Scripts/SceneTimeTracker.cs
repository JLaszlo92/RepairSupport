using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

[Serializable]
public class SceneTimeEntry
{
    public string sceneName;
    public float timeSpent; // In seconds
}

[Serializable]
public class SceneTimeData
{
    public List<SceneTimeEntry> entries = new List<SceneTimeEntry>();
}

public class SceneTimeTracker : MonoBehaviour
{
    public static SceneTimeTracker Instance;

    private float sceneStartTime;
    private string currentSceneName;
    private SceneTimeData timeData = new SceneTimeData();

    private string filePath;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            filePath = Path.Combine(Application.persistentDataPath, "SceneTimeData.json");

            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentSceneName = scene.name;
        sceneStartTime = Time.time;
    }

    private void OnSceneUnloaded(Scene scene)
    {
        float timeSpent = Time.time - sceneStartTime;
        AddTimeEntry(currentSceneName, timeSpent);
        SaveToJson();
    }

    void AddTimeEntry(string sceneName, float time)
    {
        var existing = timeData.entries.Find(e => e.sceneName == sceneName);
        if (existing != null)
        {
            existing.timeSpent += time;
        }
        else
        {
            timeData.entries.Add(new SceneTimeEntry
            {
                sceneName = sceneName,
                timeSpent = time
            });
        }
    }

    void SaveToJson()
    {
        string json = JsonUtility.ToJson(timeData, true);
        File.WriteAllText(filePath, json);
        Debug.Log($"✅ Scene times saved to: {filePath}");
    }

    public SceneTimeData GetSceneTimeData()
    {
        return timeData;
    }
}
