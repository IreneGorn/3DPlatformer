using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour, IGameManager
{
    private ILevelGenerator _levelGenerator;
    private IPlayerController _playerController;
    private IUIManager _uiManager;
    private float _startTime;

    [Inject]
    public void Construct(ILevelGenerator levelGenerator, IPlayerController playerController, IUIManager uiManager)
    {
        _levelGenerator = levelGenerator;
        _playerController = playerController;
        _uiManager = uiManager;
    }

    public void StartGame()
    {
        Debug.Log("Game started");
        _levelGenerator.GenerateLevel();
        _playerController.ResetPlayer();
        _startTime = Time.time;
        _uiManager.UpdateHealthDisplay(_playerController.GetHealth());
    }

    public void EndGame(bool isVictory)
    {
        Debug.Log("Game ended. Victory: " + isVictory);
        float elapsedTime = GetElapsedTime();
        _uiManager.ShowEndGameScreen(isVictory, elapsedTime);
    }

    public float GetElapsedTime()
    {
        return Time.time - _startTime;
    }
}