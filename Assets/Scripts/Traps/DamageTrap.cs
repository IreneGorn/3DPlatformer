using System.Collections;
using UnityEngine;

public class DamageTrap : Trap
{
    [SerializeField] private float _activationDelay = 1f;
    [SerializeField] private float _cooldownTime = 5f;
    [SerializeField] private int _damageAmount = 10;

    private Renderer _renderer;
    private Color _originalColor;
    private bool _isActive = false;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _originalColor = _renderer.material.color;
    }

    protected override void Activate(Collision other)
    {
        if (!_isActive)
        {
            _isActive = true;
            _renderer.material.color = new Color(255, 128, 0);
            StartCoroutine(ActivationSequence());
        }
    }

    protected override void Deactivate(Collision other)
    {
        
    }

    private IEnumerator ActivationSequence()
    {
        yield return new WaitForSeconds(_activationDelay);

        _renderer.material.color = Color.red;
        ApplyDamage();

        yield return new WaitForSeconds(0.2f);

        _renderer.material.color = _originalColor;
        _isActive = false;

        yield return new WaitForSeconds(_cooldownTime);

        _isActive = false;
    }

    private void ApplyDamage()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, transform.localScale / 2, Quaternion.identity, LayerMask.GetMask("Player"));
        foreach (Collider collider in colliders)
        {
            IPlayerController player = collider.GetComponent<IPlayerController>();
            if (player != null)
            {
                player.TakeDamage(_damageAmount);
            }
        }
    }
}