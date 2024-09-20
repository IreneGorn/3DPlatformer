using UnityEngine.Events;

public interface IPlayerController
{
    void ResetPlayer();
    int GetHealth();
    void TakeDamage(int amount);
    
    event UnityAction OnPlayerDeath;
    event UnityAction OnPlayerWin;
    event UnityAction<int> OnPlayerHealthChanged;
}
