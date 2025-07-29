using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SceneMenuEvents : MonoBehaviour
{
    private UIDocument _document;
    private Button _button;

    private void Awake()
    {
        _document = GetComponent<UIDocument>();

        _button = _document.rootVisualElement.Q("NextSceneButton") as Button;
        _button.RegisterCallback<ClickEvent>(OnNextSceneClick);
    }

    private void OnDisable()
    {
        _button.UnregisterCallback<ClickEvent>(OnNextSceneClick);
    }

    private void OnNextSceneClick(ClickEvent evt)
    {
        Debug.Log("You pressed the Start Button");
    }

}
