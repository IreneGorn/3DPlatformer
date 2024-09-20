using System.Collections;
using UnityEngine;

public class SpikeTrap : Trap
{
    [SerializeField] private float _spikeDuration = 0.5f; 
    [SerializeField] private float _cooldownTime = 5f;  
    [SerializeField] private int _damageAmount = 10;     
    [SerializeField] private Transform _spikes;      
    [SerializeField] private Vector3 _spikeExtendedPosition;
    [SerializeField] private Vector3 _spikeHiddenPosition;   

    private GameObject _player;
    private bool _playerOnTrap = false;

    private void Start()
    {
        StartCoroutine(SpikeRoutine());
    }

    private IEnumerator SpikeRoutine()
    {
        while (true)
        {
            _spikes.localPosition = _spikeExtendedPosition;
            
            if (_playerOnTrap && _player != null)
            {
                _player.GetComponent<PlayerController>().TakeDamage(_damageAmount);
            }

            yield return new WaitForSeconds(_spikeDuration);

            _spikes.localPosition = _spikeHiddenPosition;

            yield return new WaitForSeconds(_cooldownTime - _spikeDuration);
        }
    }

    protected override void Activate(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _player = other.gameObject;
            _playerOnTrap = true;
        }    
    }

    protected override void Deactivate(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _playerOnTrap = false;
            _player = null;
        }
    }
}