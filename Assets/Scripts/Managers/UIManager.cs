using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIManager : MonoBehaviour, IUIManager
{
    [SerializeField] private TextMeshProUGUI _endGameText;
    [SerializeField] private Button _restartButton;
    [SerializeField] private GameObject _endGamePanel;
    [SerializeField] private TextMeshProUGUI _hp;
    
    public void SetRestartAction(UnityAction restartAction)
    {
        _restartButton.onClick.RemoveAllListeners();
        _restartButton.onClick.AddListener(restartAction);
    }
    
    public void UpdateHealthDisplay(int health)
    {
        _hp.text = $"HP: {health}";
    }

    public void ShowEndGameScreen(bool isVictory, float elapsedTime)
    {
        _endGameText.text = isVictory ? "Победа!\nСыграть ещё раз?" : "Поражение!\nСыграть ещё раз?";
        _endGamePanel.SetActive(true);
    }
    
    public void HideEndGameScreen()
    {
        _endGamePanel.SetActive(false);
    }
}
