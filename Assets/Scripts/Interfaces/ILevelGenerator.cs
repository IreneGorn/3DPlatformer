using UnityEngine.Events;

public interface ILevelGenerator
{
    void GenerateLevel();
    
    event UnityAction OnLevelGenerated;
}
