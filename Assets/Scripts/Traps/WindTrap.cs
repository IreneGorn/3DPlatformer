using System.Collections;
using UnityEngine;

public class WindTrap : Trap
{
    [SerializeField] private float _windForce = 5f;
    [SerializeField] private float _directionChangeInterval = 2f;

    private Vector3 _currentWindDirection;
    private Rigidbody _playerRigidbody;

    private void Start()
    {
        StartCoroutine(ChangeWindDirection());
    }

    protected override void Activate()
    {
        if (_playerRigidbody == null)
        {
            _playerRigidbody = FindObjectOfType<PlayerController>().GetComponent<Rigidbody>();
        }
    }

    protected override void Deactivate()
    {
        _playerRigidbody = null;
    }

    private void FixedUpdate()
    {
        if (_playerRigidbody != null)
        {
            _playerRigidbody.AddForce(_currentWindDirection * _windForce);
        }
    }

    private IEnumerator ChangeWindDirection()
    {
        while (true)
        {
            _currentWindDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
            yield return new WaitForSeconds(_directionChangeInterval);
        }
    }
}