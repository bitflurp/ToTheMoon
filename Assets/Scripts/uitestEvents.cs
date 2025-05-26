using UnityEngine;
using UnityEngine.UIElements;

public class uitestEvents : MonoBehaviour
{
    private UIDocument uiDocument;
    private Button _button;

    void Awake()
    {
        uiDocument = GetComponent<UIDocument>();
        _button = uiDocument.rootVisualElement.Q("Escape") as Button;
        _button.RegisterCallback<ClickEvent>(OnPlayGameClick);
    }

    void OnDisable()
    {
        _button.UnregisterCallback <ClickEvent>(OnPlayGameClick);
    }
    private void OnPlayGameClick(ClickEvent evt)
    {
        Debug.Log("Escape");
    }
}
