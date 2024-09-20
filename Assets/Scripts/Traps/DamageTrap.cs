using System.Collections;
using UnityEngine;

public class DamageTrap : Trap
{
    [SerializeField] private float _activationDelay = 1f;
    [SerializeField] private float _cooldownTime = 2f;
    [SerializeField] private int _damageAmount = 10;

    private Renderer _renderer;
    private Color _originalColor;
    private GameObject _player;
    private bool _isActive = false;
    private bool _playerOnTrap = false;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _originalColor = _renderer.material.color;
    }

    protected override void Activate(Collision other)
    {
        _player = other.gameObject;
        _playerOnTrap = true;
        
        if (!_isActive)
        {
            StartCoroutine(ActivationSequence());
        }
    }

    protected override void Deactivate(Collision other)
    {
        _playerOnTrap = false;
    }

    private IEnumerator ActivationSequence()
    {
        _isActive = true;
        _renderer.material.color = new Color(255, 128, 0);

        yield return new WaitForSeconds(_activationDelay);

        _renderer.material.color = Color.red;
        ApplyDamage();

        yield return new WaitForSeconds(0.5f);

        _renderer.material.color = _originalColor;
        _isActive = false;

        yield return new WaitForSeconds(_cooldownTime);

        if (_playerOnTrap)
        {
            StartCoroutine(ActivationSequence());
        }
    }

    private void ApplyDamage()
    {
        if (_isActive)
        {
            if (_player != null)
            {
                _player.GetComponent<PlayerController>().TakeDamage(_damageAmount);
            }
        }
    }
}