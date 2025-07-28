using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;
    public SceneConfig config;


    public static string mainFolderPath = "Data"; 
    public static string mainFileName = "SceneConfig";
    private TextAsset jsonFile;

    public string TeamColor = "Black";
    public int currentIndex = 0;

    public string[] sceneList;

    private void Awake()
    {


        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);


        // Load JSON file safely inside Awake()
        jsonFile = Resources.Load<TextAsset>($"{mainFolderPath}/{mainFileName}");
        if (jsonFile == null)
        {
            Debug.LogError("❌ Config file not found in Resources!");
            return;
        }
        
        Debug.Log("✅ JSON file loaded successfully!");

        
        // Optionally parse it now or store it for later

        config = JsonUtility.FromJson<SceneConfig>(jsonFile.text);
        sceneList = config.sceneList;
        currentIndex = config.currentIndex;

        Debug.Log("✅ Loaded scenes: " + config.sceneList.Length);
        
        Debug.Log("Basic TeamColor set to: " + MainManager.Instance.TeamColor);

    }
}
