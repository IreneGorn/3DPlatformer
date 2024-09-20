using UnityEngine.Events;

public interface IUIManager
{
    void SetRestartAction(UnityAction restartAction);
    void UpdateHealthDisplay(int health);
    void ShowEndGameScreen(bool isVictory, float elapsedTime);
    void HideEndGameScreen();
}
