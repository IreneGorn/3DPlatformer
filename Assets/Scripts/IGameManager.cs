public interface IGameManager
{
    void StartGame();
    void EndGame(bool isVictory);
    float GetElapsedTime();
}