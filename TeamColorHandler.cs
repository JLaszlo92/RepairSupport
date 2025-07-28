using UnityEngine;

public class TeamColorHandler : MonoBehaviour
{
    public string NewColor; // Inside Resources/

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Read current color
        Debug.Log("Current TeamColor: " + MainManager.Instance.TeamColor);

        // Change it based on game logic
        MainManager.Instance.TeamColor = NewColor;
        Debug.Log("New TeamColor set to: " + MainManager.Instance.TeamColor);
        Debug.Log("Current scene index: " + MainManager.Instance.currentIndex);

        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
