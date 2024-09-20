using UnityEngine;
using Zenject;

public class LevelGenerator : MonoBehaviour, ILevelGenerator
{
    [Inject]
    private ITrapManager trapManager;

    public void GenerateLevel()
    {
        Debug.Log("Generating level...");
        // Здесь будет логика генерации уровня
        // Например:
        CreateStartPlatform();
        CreatePaths();
        CreateFinishPlatform();
        trapManager.InitializeTraps();
    }

    private void CreateStartPlatform()
    {
        // Логика создания стартовой платформы
    }

    private void CreatePaths()
    {
        // Логика создания путей
    }

    private void CreateFinishPlatform()
    {
        // Логика создания финишной платформы
    }
}