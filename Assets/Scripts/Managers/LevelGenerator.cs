using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class LevelGenerator : MonoBehaviour, ILevelGenerator
{
    [SerializeField] private GameObject _platformPrefab;
    [SerializeField] private GameObject _startPlatformPrefab;
    [SerializeField] private GameObject _finishPlatformPrefab;
    [SerializeField] private GameObject[] _trapPrefabs;

    [SerializeField] private int _platformsBeforeFinish = 20;
    [SerializeField] private float _platformSpacing = 4f;
    [SerializeField] private float _finishDistance = 8f;
    [SerializeField] private int _trapCount = 12;
    [SerializeField] private float _pathWidth = 4f;
    [SerializeField] private float _curveStrength = 12f;

    private int _trapsLeft;

    private readonly List<GameObject> _generatedPlatforms = new List<GameObject>();
    
    public event UnityAction OnLevelGenerated;

    public void GenerateLevel()
    {
        Debug.Log("Generating level...");
        
        ClearExistingPlatforms();

        Vector3 startPosition = new Vector3(0, 3, 0);
        GameObject startPlatform = Instantiate(_startPlatformPrefab, startPosition, Quaternion.identity);
        _generatedPlatforms.Add(startPlatform);
        
        Vector3 currentLeftPathPosition = startPosition + new Vector3(-_pathWidth, 0, _platformSpacing);
        Vector3 currentRightPathPosition = startPosition + new Vector3(_pathWidth, 0, _platformSpacing);

        for (int i = 0; i < _platformsBeforeFinish / 2; i++)
        {
            float curveOffset = Mathf.Sin((float)i / (_platformsBeforeFinish / 2) * Mathf.PI) * _curveStrength;

            Vector3 leftPosition = currentLeftPathPosition + new Vector3(-curveOffset, 0, _platformSpacing);
            CreatePlatformOrTrap(leftPosition);
            currentLeftPathPosition += new Vector3(0, 0, _platformSpacing);

            Vector3 rightPosition = currentRightPathPosition + new Vector3(curveOffset, 0, _platformSpacing);
            CreatePlatformOrTrap(rightPosition);
            currentRightPathPosition += new Vector3(0, 0, _platformSpacing);
        }

        Vector3 finishPosition = (currentLeftPathPosition + currentRightPathPosition) / 2;
        finishPosition.z += _finishDistance;

        GameObject finishPlatform = Instantiate(_finishPlatformPrefab, finishPosition, Quaternion.identity);
        _generatedPlatforms.Add(finishPlatform);
        
        OnLevelGenerated?.Invoke();
    }

    private void CreatePlatformOrTrap(Vector3 position)
    {
        GameObject platform;

        if (_trapsLeft > 0 && Random.value < 0.6f)
        {
            GameObject trapPrefab = _trapPrefabs[Random.Range(0, _trapPrefabs.Length)];
            platform = Instantiate(trapPrefab, position, Quaternion.identity);
            _trapsLeft--;
        }
        else
        {
            platform = Instantiate(_platformPrefab, position, Quaternion.identity);
        }

        _generatedPlatforms.Add(platform);
    }

    private void ClearExistingPlatforms()
    {
        foreach (var platform in _generatedPlatforms)
        {
            Destroy(platform);
        }
        _generatedPlatforms.Clear();

        _trapsLeft = _trapCount;
    }
}