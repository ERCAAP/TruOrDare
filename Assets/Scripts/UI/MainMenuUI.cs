using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;
    
    private void Awake()
    {
        SetupButtons();
    }
    
    private void SetupButtons()
    {
        startButton.onClick.AddListener(OnStartClicked);
        settingsButton.onClick.AddListener(OnSettingsClicked);
        quitButton.onClick.AddListener(OnQuitClicked);
    }
    
    private void OnStartClicked()
    {
        UIManager.Instance.ShowPanel(PanelType.PlayerSelection);
    }
    
    private void OnSettingsClicked()
    {
        UIManager.Instance.ShowPanel(PanelType.Settings);
    }
    
    private void OnQuitClicked()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
} 