public interface IUIManager
{
    void UpdateHealthDisplay(int health);
    void ShowEndGameScreen(bool isVictory, float elapsedTime);
}
