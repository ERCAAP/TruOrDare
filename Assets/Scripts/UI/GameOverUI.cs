using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Transform playerResultsContainer;
    [SerializeField] private GameObject playerResultPrefab;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button mainMenuButton;
    
    private void Awake()
    {
        SetupButtons();
    }

    private void OnEnable()
    {
        ShowResults();
    }

    private void SetupButtons()
    {
        restartButton.onClick.AddListener(OnRestartClicked);
        mainMenuButton.onClick.AddListener(OnMainMenuClicked);
    }

    private void ShowResults()
    {
        // Clear existing results
        foreach (Transform child in playerResultsContainer)
        {
            Destroy(child.gameObject);
        }

        // Sort players by points
        var sortedPlayers = GameManager.Instance.Players
            .OrderByDescending(p => p.Points)
            .ToList();

        // Create result items
        foreach (var player in sortedPlayers)
        {
            var resultObj = Instantiate(playerResultPrefab, playerResultsContainer);
            var resultUI = resultObj.GetComponent<PlayerResultUI>();
            resultUI.Initialize(player);
        }
    }

    private void OnRestartClicked()
    {
        GameManager.Instance.RestartGame();
    }

    private void OnMainMenuClicked()
    {
        GameManager.Instance.ReturnToMainMenu();
    }
} 