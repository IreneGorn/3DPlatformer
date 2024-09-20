using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIManager : MonoBehaviour, IUIManager
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private GameObject _endGamePanel;
    
    public void SetRestartAction(UnityAction restartAction)
    {
        _restartButton.onClick.RemoveAllListeners();
        _restartButton.onClick.AddListener(restartAction);
    }
    
    public void UpdateHealthDisplay(int health)
    {
        // Обновление отображения здоровья
    }

    public void ShowEndGameScreen(bool isVictory, float elapsedTime)
    {
        _endGamePanel.SetActive(true);
    }
    
    public void HideEndGameScreen()
    {
        _endGamePanel.SetActive(false);
    }
}
