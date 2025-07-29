using UnityEngine;
using UnityEngine.SceneManagement;

public class JSONLoader : MonoBehaviour
{
    public string folderPath = "Data"; // Inside Resources/
    private string fileName;

    void Start()
    {

        fileName = MainManager.Instance.sceneList[MainManager.Instance.currentIndex];
        Debug.Log($"✅ FileName: '{fileName}'");


        TextAsset jsonFile = Resources.Load<TextAsset>($"{folderPath}/{fileName}"); // No .json extension
        if (jsonFile == null)
        {
            Debug.LogError("❌ Config file not found!");
            return;
        }

        SceneWrapper wrapper = JsonUtility.FromJson<SceneWrapper>(jsonFile.text);
        if (wrapper.marks == null || wrapper.marks.Length == 0)
        {
            Debug.LogError("❌ No marks found in the config!");
            return;
        }

        foreach (var c in wrapper.marks)
        {
            GameObject prefab = Resources.Load<GameObject>($"Prefabs/{c.name}");
            if (prefab == null)
            {
                Debug.LogError($"❌ Could not find prefab: {c.name}");
                continue;
            }

            GameObject parentObject = GameObject.Find("Capsule");
            if (parentObject == null)
            {
                Debug.LogError("❌ Could not find parent object 'Capsule'");
                return;
            }

            if (c.StartPosition == null || c.StartPosition.Length != 3)
            {
                Debug.LogError("❌ StartPosition missing or invalid.");
                continue;
            }

            Vector3 position = new Vector3(c.StartPosition[0], c.StartPosition[1], c.StartPosition[2]);

            if (c.StartRotation == null || c.StartRotation.Length != 3)
            {
                Debug.LogError("❌ Invalid StartRotation.");
                continue;
            }

            Vector3 euler = new Vector3(c.StartRotation[0], c.StartRotation[1], c.StartRotation[2]);
            Quaternion localRot = Quaternion.Euler(euler);

            GameObject instance = Instantiate(prefab, parentObject.transform);
            instance.transform.localPosition = position;
            instance.transform.localRotation = localRot;

            Debug.Log($"✅ Spawned '{c.name}' under '{parentObject.name}'");
        }
    }
}
