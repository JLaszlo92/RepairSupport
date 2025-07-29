using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private TextAsset sceneJson;
    public string mainFolderPath = "Data/Devices"; // Inside Resources/
    public SceneConfig config;


   public void PlayGame()
   {
     //SceneManager.LoadSceneAsync("Scene0");
   }
   public void BacktoMainMenu()
   {
    SceneManager.LoadSceneAsync("MainMenu");
   }
   public void PreviousScene()
   {
     if (MainManager.Instance.currentIndex == 0)
     {
          Debug.Log("❌ Already at first scene, cannot go back.");
          return; // Prevent changes
     }
     MainManager.Instance.currentIndex -= 1;
     ChosenSceneLoader();
     }
   public void NextScene()
   {
     if (MainManager.Instance.currentIndex + 1 >= MainManager.Instance.sceneList.Length)
     {
          SceneManager.LoadScene("LastPage");
          return;
     }
     MainManager.Instance.currentIndex += 1;
     ChosenSceneLoader();
     }

    public void Exit()
   {
         Application.Quit();
         
   }

    private void ChosenSceneLoader()
   {
     string FolderPathForScene = $"{mainFolderPath}/{MainManager.Instance.selectedDevice}/{MainManager.Instance.selectedScene}";
     string sceneName = MainManager.Instance.sceneList[MainManager.Instance.currentIndex];

     sceneJson = Resources.Load<TextAsset>($"{FolderPathForScene}/{sceneName}");

     if (sceneJson == null)
     {
          Debug.Log("❌ Config file not found in Resources!");
          return;
     }

        
     Debug.Log("✅ Scene JSON file loaded successfully!");

        
        // Optionally parse it now or store it for later

     SceneWrapper wrapper = JsonUtility.FromJson<SceneWrapper>(sceneJson.text);
     string currentScene = wrapper.header.sceneName;

     SceneManager.LoadScene(currentScene);
   }
   

}
