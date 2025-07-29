using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.IO;

public class SessionSummaryUI : MonoBehaviour
{
    [System.Serializable]
    public class SceneEntry
    {
        public string sceneName;
        public float timeSpent;
    }

    [System.Serializable]
    public class SessionLog
    {
        public List<SceneEntry> entries = new List<SceneEntry>();
    }

    public TMP_Text summaryText; // Drag TextMeshPro UI element here in Inspector

    private string filePath;

    void Start()
    {
        filePath = Path.Combine(Application.persistentDataPath, "SceneTimeData.json");
        LoadAndDisplaySummary();
    }

    void LoadAndDisplaySummary()
    {
        if (!File.Exists(filePath))
        {
            Debug.LogError($"❌ Summary file not found at: {filePath}");
            summaryText.text = "No session summary found.";
            return;
        }

        string json = File.ReadAllText(filePath);
        SessionLog log = JsonUtility.FromJson<SessionLog>(json);

        if (log == null || log.entries == null || log.entries.Count == 0)
        {
            summaryText.text = "No session data recorded.";
            return;
        }

        string displayText = "<b> Repair Session Summary</b>\n\n";
        foreach (var entry in log.entries)
        {
            displayText += $"{entry.sceneName} - {entry.timeSpent:F2}s\n";
        }

        summaryText.text = displayText;
    }
}
