using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelGenerator : MonoBehaviour, ILevelGenerator
{
    [SerializeField] private GameObject platformPrefab;
    [SerializeField] private GameObject startPlatformPrefab;
    [SerializeField] private GameObject finishPlatformPrefab;
    [SerializeField] private GameObject[] trapPrefabs;

    [SerializeField] private int platformsBeforeFinish = 20; // Количество платформ до финиша
    [SerializeField] private float platformSpacing = 3f; // Расстояние между платформами
    [SerializeField] private float finishDistance = 10f; // Расстояние до финиша
    [SerializeField] private int trapCount = 5; // Количество ловушек

    private List<GameObject> generatedPlatforms = new List<GameObject>();
    
    public event UnityAction OnLevelGenerated;

    public void GenerateLevel()
    {
        Debug.Log("Generating level...");
        
        ClearExistingPlatforms();

        Vector3 startPosition = new Vector3(0, 3, 0);
        GameObject startPlatform = Instantiate(startPlatformPrefab, startPosition, Quaternion.identity);
        generatedPlatforms.Add(startPlatform);

        // Генерация троп
        Vector3 currentLeftPathPosition = startPosition + new Vector3(-1.5f, 0, platformSpacing);
        Vector3 currentRightPathPosition = startPosition + new Vector3(1.5f, 0, platformSpacing);

        for (int i = 0; i < platformsBeforeFinish / 2; i++)
        {
            // Генерация левой тропы
            CreatePlatformOrTrap(currentLeftPathPosition);
            currentLeftPathPosition += new Vector3(0, 0, platformSpacing);

            // Генерация правой тропы
            CreatePlatformOrTrap(currentRightPathPosition);
            currentRightPathPosition += new Vector3(0, 0, platformSpacing);
        }

        // Соединение троп и финишная платформа
        Vector3 finishPosition = new Vector3(0, 0, currentLeftPathPosition.z + finishDistance);
        GameObject finishPlatform = Instantiate(finishPlatformPrefab, finishPosition, Quaternion.identity);
        generatedPlatforms.Add(finishPlatform);
        
        OnLevelGenerated?.Invoke();
    }

    private void CreatePlatformOrTrap(Vector3 position)
    {
        GameObject platform;

        // Решаем, будет ли платформа ловушкой
        if (trapCount > 0 && Random.value < 0.3f) // Вероятность генерации ловушки
        {
            GameObject trapPrefab = trapPrefabs[Random.Range(0, trapPrefabs.Length)];
            platform = Instantiate(trapPrefab, position, Quaternion.identity);
            trapCount--;
        }
        else
        {
            platform = Instantiate(platformPrefab, position, Quaternion.identity);
        }

        generatedPlatforms.Add(platform);
    }

    private void ClearExistingPlatforms()
    {
        foreach (var platform in generatedPlatforms)
        {
            Destroy(platform);
        }
        generatedPlatforms.Clear();
    }
}