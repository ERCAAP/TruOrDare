using UnityEngine;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public void ShowMessage(string message)
        {
            // TODO: Implement message popup
            Debug.Log(message);
        }

        public void ShowPremiumPrompt()
        {
            // TODO: Show premium features panel
        }

        public void ShowInputDialog(string title, string message, System.Action<string> onConfirm)
        {
            // TODO: Implement input dialog
        }

        public void ShowAvatarSelectionPanel()
        {
            // TODO: Implement avatar selection
        }

        public void ShowAddPlayerPrompt()
        {
            // TODO: Implement add player prompt
        }
    }
} 