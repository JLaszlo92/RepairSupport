using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private TextAsset sceneJson;
    public string folderPath = "Data"; // Inside Resources/
    public SceneConfig config;


   public void PlayGame()
   {
    SceneManager.LoadSceneAsync("Scene0");
   }
   public void BacktoMainMenu()
   {
    SceneManager.LoadSceneAsync("MainMenu");
   }
   public void PreviousScene()
   {
        MainManager.Instance.currentIndex -= 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
   }
   public void NextScene()
   {    
        MainManager.Instance.currentIndex += 1;

        string sceneName = MainManager.Instance.sceneList[MainManager.Instance.currentIndex];
        sceneJson = Resources.Load<TextAsset>($"{folderPath}/{sceneName}");

        if (sceneJson == null)
        {
            Debug.LogError("❌ Config file not found in Resources!");
            return;
        }
        
        Debug.Log("✅ Scene JSON file loaded successfully!");

        
        // Optionally parse it now or store it for later

        SceneWrapper wrapper = JsonUtility.FromJson<SceneWrapper>(sceneJson.text);
        string currentScene = wrapper.header.sceneName;

        SceneManager.LoadScene(currentScene);

        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
   }

    public void Exit()
   {
         Application.Quit();
   }

}
