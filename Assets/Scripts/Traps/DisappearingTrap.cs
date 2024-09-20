using System.Collections;
using UnityEngine;

public class DisappearingTrap : Trap
{
    [SerializeField] private float _disappearTime = 3f;        
    
    private Renderer _renderer;            
    private Collider _collider;              
    private Color _originalColor;
    private bool _isPlayerOnPlatform = false;
    private bool _isFading = false;

    private void Start()
    {
        if (_renderer == null)
            _renderer = GetComponent<Renderer>();

        if (_collider == null)
            _collider = GetComponent<Collider>();

        _originalColor = _renderer.material.color;
    }

    protected override void Activate(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && !_isFading)
        {
            _isPlayerOnPlatform = true;
            StartCoroutine(FadeAndDisappear());
        }
    }

    protected override void Deactivate(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _isPlayerOnPlatform = false;
            StopAllCoroutines();
            ResetPlatform();
        }    
    }

    private IEnumerator FadeAndDisappear()
    {
        _isFading = true;
        float elapsedTime = 0f;

        while (elapsedTime < _disappearTime && _isPlayerOnPlatform)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / _disappearTime);
            SetPlatformTransparency(alpha);
            yield return null;
        }

        if (_isPlayerOnPlatform)
        {
            _collider.enabled = false; 
            _renderer.enabled = false; 
        }

        _isFading = false;
    }

    private void SetPlatformTransparency(float alpha)
    {
        Color color = _renderer.material.color;
        color.a = alpha;
        _renderer.material.color = color;
    }

    private void ResetPlatform()
    {
        SetPlatformTransparency(1f);
        _collider.enabled = true;
        _renderer.enabled = true;
    }
}