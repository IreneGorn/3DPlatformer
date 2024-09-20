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
        
        _levelGenerator.OnLevelGenerated += OnLevelGenerated;

        _playerController.OnPlayerDeath += HandlePlayerDeath;
        _playerController.OnPlayerWin += HandlePlayerWin;
        _playerController.OnPlayerHealthChanged += UpdateUIHealth;
    }

    private void Start()
    {
        OnGameStart += StartGame;
        OnGameEnd += EndGame;
        
        StartGame();
    }

    private void OnDestroy()
    {
        _playerController.OnPlayerDeath -= HandlePlayerDeath;
        _playerController.OnPlayerWin -= HandlePlayerWin;
        _playerController.OnPlayerHealthChanged -= UpdateUIHealth;
    }
    
    private void OnLevelGenerated()
    {
        _playerController.ResetPlayer();
        _startTime = Time.time;
        _uiManager.UpdateHealthDisplay(_playerController.GetHealth());
        _uiManager.HideEndGameScreen();
    }

    public void StartGame()
    {
        Debug.Log("Game started");
        _levelGenerator.GenerateLevel();
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
    
    private void HandlePlayerWin()
    {
        EndGame(true);
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