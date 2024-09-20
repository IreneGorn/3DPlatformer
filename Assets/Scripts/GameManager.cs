using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class GameManager : MonoBehaviour, IGameManager
{
    private ILevelGenerator _levelGenerator;
    private IPlayerController _playerController;
    private IUIManager _uiManager;
    private float _startTime;

    public UnityAction OnGameStart;
    public UnityAction<bool> OnGameEnd;

    [Inject]
    public void Construct(ILevelGenerator levelGenerator, IPlayerController playerController, IUIManager uiManager)
    {
        _levelGenerator = levelGenerator;
        _playerController = playerController;
        _uiManager = uiManager;

        _playerController.OnPlayerDeath += HandlePlayerDeath;
        _playerController.OnPlayerHealthChanged += UpdateUIHealth;
    }

    private void Start()
    {
        OnGameStart += StartGame;
        OnGameEnd += EndGame;
    }

    private void OnDestroy()
    {
        _playerController.OnPlayerDeath -= HandlePlayerDeath;
        _playerController.OnPlayerHealthChanged -= UpdateUIHealth;
    }

    public void StartGame()
    {
        Debug.Log("Game started");
        _levelGenerator.GenerateLevel();
        _playerController.ResetPlayer();
        _startTime = Time.time;
        _uiManager.UpdateHealthDisplay(_playerController.GetHealth());
        _uiManager.HideEndGameScreen();
    }

    public void EndGame(bool isVictory)
    {
        Debug.Log("Game ended. Victory: " + isVictory);
        float elapsedTime = GetElapsedTime();
        _uiManager.ShowEndGameScreen(isVictory, elapsedTime);
        _uiManager.SetRestartAction(StartGame);
    }

    private void HandlePlayerDeath()
    {
        EndGame(false);
    }

    private void UpdateUIHealth(int currentHealth)
    {
        _uiManager.UpdateHealthDisplay(currentHealth);
    }

    public float GetElapsedTime()
    {
        return Time.time - _startTime;
    }
}