using System;
using UnityEngine;

public abstract class Trap : MonoBehaviour
{
    protected abstract void Activate(Collision other);
    protected abstract void Deactivate(Collision other);

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Activate(other);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Deactivate(other);
        }
    }
}