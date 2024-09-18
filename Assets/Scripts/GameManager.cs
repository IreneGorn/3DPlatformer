using UnityEngine;

public class GameManager : IGameManager
{
    private readonly ILevelGenerator _levelGenerator;
    private readonly IPlayerController _playerController;
    private readonly IUIManager _uiManager;
    private float _startTime;

    public GameManager(ILevelGenerator levelGenerator, IPlayerController playerController, IUIManager uiManager)
    {
        _levelGenerator = levelGenerator;
        _playerController = playerController;
        _uiManager = uiManager;
    }

    public void StartGame()
    {
        _levelGenerator.GenerateLevel();
        _playerController.ResetPlayer();
        _startTime = Time.time;
        _uiManager.UpdateHealthDisplay(_playerController.GetHealth());
    }

    public void EndGame(bool isVictory)
    {
        float elapsedTime = GetElapsedTime();
        _uiManager.ShowEndGameScreen(isVictory, elapsedTime);
    }

    public float GetElapsedTime()
    {
        return Time.time - _startTime;
    }
}