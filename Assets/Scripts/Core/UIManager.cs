using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject gameplayPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject premiumPromptPanel;
    [SerializeField] private GameObject messagePanel;
    [SerializeField] private GameObject avatarSelectionPanel;
    [SerializeField] private GameObject inputDialogPanel;
    
    private Stack<GameObject> panelStack = new Stack<GameObject>();
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void Initialize()
    {
        ShowPanel(mainMenuPanel);
    }
    
    public void ShowPanel(GameObject panel)
    {
        if (panelStack.Count > 0)
        {
            panelStack.Peek().SetActive(false);
        }
        
        panel.SetActive(true);
        panelStack.Push(panel);
    }

    public void ShowPanel(PanelType panelType)
    {
        GameObject panel = GetPanelByType(panelType);
        if (panel != null)
        {
            ShowPanel(panel);
        }
    }
    
    public void ShowMessage(string message)
    {
        // TODO: Implement message display
        Debug.Log(message);
    }
    
    public void ShowPremiumPrompt()
    {
        ShowPanel(premiumPromptPanel);
    }
    
    public void ShowInputDialog(string title, string message, System.Action<string> onConfirm)
    {
        // TODO: Implement input dialog
    }
    
    public void ShowAvatarSelectionPanel()
    {
        if (avatarSelectionPanel != null)
        {
            ShowPanel(avatarSelectionPanel);
        }
    }
    
    public void ShowAddPlayerPrompt()
    {
        // TODO: Implement add player prompt
    }
    
    private GameObject GetPanelByType(PanelType type)
    {
        return type switch
        {
            PanelType.MainMenu => mainMenuPanel,
            PanelType.Settings => settingsPanel,
            PanelType.GameOver => gameplayPanel,
            PanelType.Premium => premiumPromptPanel,
            _ => null
        };
    }
    
    public void GoBack()
    {
        if (panelStack.Count > 1)
        {
            GameObject currentPanel = panelStack.Pop();
            currentPanel.SetActive(false);
            
            GameObject previousPanel = panelStack.Peek();
            previousPanel.SetActive(true);
        }
    }
    
    public void ShowLoadingScreen(bool show)
    {
        loadingScreen.SetActive(show);
    }
} 