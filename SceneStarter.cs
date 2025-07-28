using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SceneStarter : MonoBehaviour
{
    public TMP_Dropdown deviceDropdown;
    public TMP_Dropdown sceneDropdown;
    public Button confirmButton;

    // This is where the selected values will be stored
    public string selectedDevice;
    public string selectedScene;

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

        // Do something with them (e.g., print or store)
        Debug.Log($"✅ Selected Device: {selectedDevice}");
        Debug.Log($"✅ Selected Scene: {selectedScene}");

        // Example: Pass to some GameManager
        // GameManager.Instance.SetDeviceAndScene(selectedDevice, selectedScene);
    }
}
