using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindTrap : Trap
{
    [SerializeField] private float _windForce = 5f;
    [SerializeField] private float _directionChangeInterval = 2f;

    private Vector3 _currentWindDirection;
    private readonly List<Rigidbody> _affectedRigidbodies = new List<Rigidbody>();

    private void Start()
    {
        StartCoroutine(ChangeWindDirection());
    }

    protected override void Activate(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRigidbody = other.gameObject.GetComponent<Rigidbody>();
            if (playerRigidbody != null && !_affectedRigidbodies.Contains(playerRigidbody))
            {
                _affectedRigidbodies.Add(playerRigidbody);
            }
        }
    }

    protected override void Deactivate(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRigidbody = other.gameObject.GetComponent<Rigidbody>();
            if (playerRigidbody != null && _affectedRigidbodies.Contains(playerRigidbody))
            {
                _affectedRigidbodies.Remove(playerRigidbody);
            }
        }
    }

    private void FixedUpdate()
    {
        foreach (var playerRigidbody in _affectedRigidbodies)
        {
            playerRigidbody.AddForce(_currentWindDirection * _windForce, ForceMode.Force);
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