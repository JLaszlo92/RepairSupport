using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class SceneStarter : MonoBehaviour
{
    public TMP_Dropdown deviceDropdown;
    public TMP_Dropdown sceneDropdown;
    public Button confirmButton;
    
    

    // This is where the selected values will be stored
    public string selectedDevice;
    public string selectedScene;

    public static string mainFolderPath = "Data/Devices";
    public static string SceneConfigName = "SceneConfig"; 
    public TextAsset sceneJsonFile;
    private string FolderPathForScene;


    void Start()
    {
        if (deviceDropdown == null || sceneDropdown == null || confirmButton == null)
        {
            Debug.LogError("❌ Assign all dropdowns and the button in the inspector.");
            return;
        }

        // Add listener for the button
        confirmButton.onClick.AddListener(OnConfirmClicked);
    }

    void OnConfirmClicked()
    {
        // Get selected values
        selectedDevice = deviceDropdown.options[deviceDropdown.value].text;
        selectedScene = sceneDropdown.options[sceneDropdown.value].text;

        MainManager.Instance.selectedDevice = selectedDevice;
        MainManager.Instance.selectedScene = selectedScene;




        // Do something with them (e.g., print or store)
        Debug.Log($"✅ Selected Device: {MainManager.Instance.selectedDevice}");
        Debug.Log($"✅ Selected Scene: {MainManager.Instance.selectedScene}");

        //MainManager.Instance.jsonFile = Resources.Load<TextAsset>($"{mainFolderPath}/{selectedDevice}/{selectedScene}/{SceneConfigName}");
        FolderPathForScene = $"{mainFolderPath}/{selectedDevice}/{selectedScene}";
        MainManager.Instance.jsonFile = Resources.Load<TextAsset>($"{FolderPathForScene}/{SceneConfigName}");
        //MainManager.Instance.jsonFile = Resources.Load<TextAsset>($"{mainFolderPath}/{selectedDevice}/{selectedScene}/{SceneConfigName}");

    
        if (MainManager.Instance.jsonFile == null)
        {
            Debug.LogError("❌ Scene config file not found in Resources!");

            return;
        }
        
        Debug.Log("✅ Scene config file loaded successfully!");

        MainManager.Instance.config = JsonUtility.FromJson<SceneConfig>(MainManager.Instance.jsonFile.text);
        MainManager.Instance.sceneList = MainManager.Instance.config.sceneList;

        string sceneName = MainManager.Instance.sceneList[MainManager.Instance.currentIndex];
        TextAsset sceneJson = Resources.Load<TextAsset>($"{FolderPathForScene}/{sceneName}");

        if (sceneJson == null)
        {
            Debug.LogError("❌ First scene file not found in Resources!");

            return;
        }
        
        Debug.Log("✅ First scene file loaded successfully!");

        SceneWrapper wrapper = JsonUtility.FromJson<SceneWrapper>(sceneJson.text);
        string currentScene = wrapper.header.sceneName;

        SceneManager.LoadScene(currentScene);

        // Example: Pass to some GameManager
        // GameManager.Instance.SetDeviceAndScene(selectedDevice, selectedScene);
    }
}
